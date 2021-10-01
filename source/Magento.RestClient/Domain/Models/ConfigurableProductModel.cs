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
			await base.Refresh();
			await RefreshOptions();
			var children = await _context.ConfigurableProducts.GetConfigurableChildren(this.Sku);
			_children = children.Select(product => new ProductModel(_context, product.Sku)).ToList();
		}

		public override async Task SaveAsync()
		{
			await base.SaveAsync();

			if (Options.Any() && Children.Any())
			{
				foreach (var option in _options)
				{
					var attribute = await _context.Attributes.GetById(option.AttributeId);
					option.Values = Children.SelectMany(model => model.CustomAttributes)
						.Where(productAttribute => productAttribute.AttributeCode == attribute.AttributeCode)
						.Select(customAttribute => customAttribute.Value).Distinct()
						.Select(value => new Value() {ValueIndex = Convert.ToInt64(value)}).ToList();

					if (option.Id == 0)
					{
						await _context.ConfigurableProducts.CreateOption(this.Sku, option);
					}
					else
					{
						await _context.ConfigurableProducts.UpdateOption(this.Sku, option.Id, option);
					}
				}


				foreach (var child in Children)
				{
					await child.SaveAsync();
					try
					{
						await _context.ConfigurableProducts.CreateChild(this.Sku, child.Sku);
					}
					catch
					{
					}
				}

				foreach (var child in _removedChildren)
				{
					await _context.ConfigurableProducts.DeleteChild(this.Sku, child);
				}

				_removedChildren.Clear();
			}

			await Refresh();
		}


		private async Task RefreshOptions()
		{
			var options = await _context.ConfigurableProducts.GetOptions(this.Sku);
			if (options != null)
			{
				_options = options;
			}

			var attributes = new List<ProductAttribute>();
			foreach (var option in _options)
			{
				var attribute = await _context.Attributes.GetById(option.AttributeId);
				attributes.Add(attribute);
			}

			_optionAttributes = attributes;
		}


		public async Task AddConfigurableOptions(params string[] attributeCodes)
		{
			foreach (var attributeCode in attributeCodes)
			{
				var attribute = await _context.Attributes.GetByCode(attributeCode);

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