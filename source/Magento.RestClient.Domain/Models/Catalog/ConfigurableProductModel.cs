using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Domain.Exceptions;
using Serilog;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public class ConfigurableProductModel : ProductModel
	{
		public ConfigurableProductModel(IAdminContext context, string sku) : base(context, sku)
		{
		}

		private List<ProductModel> _children = new();
		private List<ProductAttribute> _optionAttributes = new();
		private List<ConfigurableProductOption> _options = new();
		private readonly IList<string> _removedChildren = new List<string>();

		public IReadOnlyList<ProductModel> Children => _children.AsReadOnly();
		public IReadOnlyList<ConfigurableProductOption> Configurations => _options.AsReadOnly();

		public override sealed async Task Refresh()
		{
			await base.Refresh().ConfigureAwait(false);
			await RefreshOptions().ConfigureAwait(false);


			var children = await Context.ConfigurableProducts.GetConfigurableChildren(this.Sku).ConfigureAwait(false);
			if (children == null)
			{
				children = new List<Product>();
			}

			_children = children.Select(product => new ProductModel(Context, product.Sku)).ToList();
		}

		public override async Task SaveAsync()
		{
			base.Type = ProductType.Configurable;
			await base.SaveAsync().ConfigureAwait(false);
			if (this.Configurations.Any() && this.Children.Any())
			{
				await SaveOptions().ConfigureAwait(false);


				await SaveChildren().ConfigureAwait(false);
			}

			await Refresh().ConfigureAwait(false);
		}

		async public Task SaveChildren()
		{
			var attachedChildren = await Context.ConfigurableProducts.GetConfigurableChildren(this.Sku);
			foreach (var child in this.Children)
			{
				child.AttributeSetId = this.AttributeSetId;
				await child.SaveAsync().ConfigureAwait(false);
				if (!attachedChildren.Select(product => product.Sku).Contains(child.Sku))
				{
					await Context.ConfigurableProducts.CreateChild(Sku, child.Sku).ConfigureAwait(false);
				}
			}


			foreach (var child in _removedChildren)
			{
				await Context.ConfigurableProducts.DeleteChild(this.Sku, child).ConfigureAwait(false);
			}

			_removedChildren.Clear();
		}

		async public Task SaveOptions()
		{
			foreach (var option in _options)
			{
				var attribute = await Context.Attributes.GetById(option.AttributeId).ConfigureAwait(false);
				option.Values = GetOptionValues(attribute.AttributeCode);


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
		}

		public List<ConfigurableProductValue> GetOptionValues(string attributecode)
		{
			return this.Children.SelectMany(model => model.CustomAttributes)
				.Where(productAttribute => productAttribute.AttributeCode == attributecode)
				.Select(customAttribute => customAttribute.Value).Distinct()
				.Select(value => new ConfigurableProductValue() {ValueIndex = Convert.ToInt64(value)})
				.ToList();
		}

		private async Task RefreshOptions()
		{
			List<ConfigurableProductOption> options = null;


			if (base.Type == ProductType.Configurable)
			{
				options = await Context.ConfigurableProducts.GetOptions(this.Sku).ConfigureAwait(false);
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
		///     GetOrCreateChild
		/// </summary>
		/// <param name="product"></param>
		/// <exception cref="ConfigurableProductInvalidException"></exception>
		/// <exception cref="ConfigurableChildInvalidException"></exception>
		public void AddChild(ProductModel product)
		{
			if (!_options.Any())
			{
				throw new ConfigurableProductInvalidException(
					"No configurable options have been defined for this product");
			}

			if (this.Children.Any(child => child.Sku == product.Sku))
			{
				Log.Warning("Child is already attached");
			}
			else
			{
				var missingAttributes = new List<string>();
				foreach (var optionAttribute in _optionAttributes)
				{
					if (product.CustomAttributes.All(attribute =>
						attribute.AttributeCode != optionAttribute.AttributeCode))
					{
						missingAttributes.Add(optionAttribute.AttributeCode);
					}
				}

				if (missingAttributes.Any())
				{
					throw new ConfigurableChildInvalidException("Missing attributes") {
						MissingAttributes = missingAttributes
					};
				}

				product.Visibility = ProductVisibility.NotVisibleIndividually;
				_children.Add(product);
			}
		}

		public void RemoveChild(string sku)
		{
			_removedChildren.Add(sku);
		}
	}
}