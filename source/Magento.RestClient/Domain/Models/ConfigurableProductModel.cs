using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Exceptions;

namespace Magento.RestClient.Domain.Models
{
	public class ConfigurableProductModel : ProductModel
	{
		public ConfigurableProductModel(IAdminContext context, string sku) : base(context, sku)
		{
			this.Type = ProductType.Configurable;
		}

		private List<ProductModel> _children = new();
		private List<ProductAttribute> _optionAttributes = new();
		private List<ConfigurableProductOption> _options = new();
		private readonly IList<string> _removedChildren = new List<string>();

		public IReadOnlyList<ProductModel> Children => _children.AsReadOnly();
		public IReadOnlyList<ConfigurableProductOption> Options => _options.AsReadOnly();

		public override sealed async Task Refresh()
		{
			await base.Refresh().ConfigureAwait(false);
			await RefreshOptions().ConfigureAwait(false);
			List<Product> children;

			children = await Context.ConfigurableProducts.GetConfigurableChildren(this.Sku).ConfigureAwait(false);
			if (children == null)
			{
				children = new List<Product>();
			}

			_children = children.Select(product => new ProductModel(Context, product.Sku)).ToList();
		}

		public override async Task SaveAsync()
		{
			await base.SaveAsync().ConfigureAwait(false);

			if (this.Options.Any() && this.Children.Any())
			{
				foreach (var option in _options)
				{
					var attribute = await Context.Attributes.GetById(option.AttributeId).ConfigureAwait(false);
					option.Values = this.Children.SelectMany(model => model.CustomAttributes)
						.Where(productAttribute => productAttribute.AttributeCode == attribute.AttributeCode)
						.Select(customAttribute => customAttribute.Value).Distinct()
						.Select(value => new Value() {ValueIndex = Convert.ToInt64(value)}).ToList();

					if (option.Id == 0)
					{
						await Context.ConfigurableProducts.CreateOption(this.Sku, option).ConfigureAwait(false);
					}
					else
					{
						await Context.ConfigurableProducts.UpdateOption(this.Sku, option.Id, option)
							.ConfigureAwait(false);
					}
				}

				foreach (var child in this.Children)
				{
					await child.SaveAsync().ConfigureAwait(false);
					try
					{
						await Context.ConfigurableProducts.CreateChild(this.Sku, child.Sku).ConfigureAwait(false);
					}
					catch
					{
					}
				}

				foreach (var child in _removedChildren)
				{
					await Context.ConfigurableProducts.DeleteChild(this.Sku, child).ConfigureAwait(false);
				}

				_removedChildren.Clear();
			}

			await Refresh().ConfigureAwait(false);
		}

		private async Task RefreshOptions()
		{
			List<ConfigurableProductOption> options;


			options = await Context.ConfigurableProducts.GetOptions(this.Sku).ConfigureAwait(false);
			if (options == null)
			{
				options = new List<ConfigurableProductOption>();
			}

			if (options != null)
			{
				_options = options;
			}

			var attributes = new List<ProductAttribute>();
			foreach (var option in _options)
			{
				var attribute = await Context.Attributes.GetById(option.AttributeId).ConfigureAwait(false);
				attributes.Add(attribute);
			}

			_optionAttributes = attributes;
		}

		public async Task AddConfigurableOptions(params string[] attributeCodes)
		{
			foreach (var attributeCode in attributeCodes)
			{
				var attribute = await Context.Attributes.GetByCode(attributeCode).ConfigureAwait(false);

				if (!_options.Any(option => option.AttributeId == attribute.AttributeId))
				{
					_options.Add(new ConfigurableProductOption {
							AttributeId = attribute.AttributeId, Label = attribute.DefaultFrontendLabel
						}
					);
				}
			}
		}

		/// <summary>
		///     AddChild
		/// </summary>
		/// <param name="product"></param>
		/// <exception cref="InvalidConfigurableProductException"></exception>
		/// <exception cref="InvalidChildProductException"></exception>
		public void AddChild(ProductModel product)
		{
			if (!_options.Any())
			{
				throw new InvalidConfigurableProductException(
					"No configurable options have been defined for this product");
			}

			if (this.Children.Any(child => child.Sku == product.Sku))
			{
				throw new ConfigurableChildAlreadyAttached();
			}

			var missingAttributes = new List<string>();
			foreach (var optionAttribute in _optionAttributes)
			{
				if (product.CustomAttributes.All(attribute => attribute.AttributeCode != optionAttribute.AttributeCode))
				{
					missingAttributes.Add(optionAttribute.AttributeCode);
				}
			}

			if (missingAttributes.Any())
			{
				throw new InvalidChildProductException("Missing attributes") {MissingAttributes = missingAttributes};
			}

			product.Visibility = ProductVisibility.NotVisibleIndividually;
			_children.Add(product);
		}

		public void RemoveChild(string sku)
		{
			_removedChildren.Add(sku);
		}
	}
}