using System;
using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Exceptions;

namespace Magento.RestClient.Domain.Models
{
	public class ConfigurableProductModel : IDomainModel
	{
		private readonly List<string> _addedChildren = new();
		private readonly IAdminContext _context;
		private readonly ProductModel _parent;
		private readonly List<string> _removedChildren = new();
		private List<Product> _children;
		private List<ProductAttribute> _optionAttributes;
		private List<ConfigurableProductOption> _options;

		public ConfigurableProductModel(IAdminContext context, string sku)
		{
			this.Sku = sku;
			_context = context;

			_parent = context.GetProductModel(sku);
			if (_parent == null)
			{
				throw new InvalidConfigurableProductException("Parent product has not been persisted.");
			}

			Refresh();
		}

		public string Sku { get; }

		public IReadOnlyList<Product> Children => _children.AsReadOnly();
		public IReadOnlyList<ConfigurableProductOption> Options => _options.AsReadOnly();

		public bool IsPersisted { get; }

		public void Refresh()
		{
			RefreshOptions();
			_children = _context.ConfigurableProducts.GetConfigurableChildren(this.Sku);
		}

		public void Save()
		{
			foreach (var option in _options)
			{
				if (option.Id == 0)
				{
					_context.ConfigurableProducts.CreateOption(this.Sku, option);
				}
			}


			foreach (var child in _addedChildren)
			{
				_context.ConfigurableProducts.CreateChild(this.Sku, child);
			}

			_addedChildren.Clear();
			foreach (var child in _removedChildren)
			{
				_context.ConfigurableProducts.DeleteChild(this.Sku, child);
			}

			_removedChildren.Clear();


			Refresh();
		}

		public void Delete()
		{
			_context.GetProductModel(this.Sku).Delete();
		}

		private void RefreshOptions()
		{
			_options = _context.ConfigurableProducts.GetOptions(this.Sku);

			var attributes = new List<ProductAttribute>();
			foreach (var option in _options)
			{
				var attribute = _context.Attributes.GetById(option.AttributeId);
				attributes.Add(attribute);
			}

			_optionAttributes = attributes;
		}


		public void AddConfigurableOption(string attributeCode)
		{
			var attribute = _context.Attributes.GetByCode(attributeCode);

			if (_options.All(option => option.AttributeId != attribute.AttributeId))
			{
				_options.Add(new ConfigurableProductOption {
						AttributeId = attribute.AttributeId,
						Label = attribute.DefaultFrontendLabel,
						Values = attribute.Options
							.Where(option => !string.IsNullOrWhiteSpace(option.Value))
							.Select(option => new Value {ValueIndex = Convert.ToInt64(option.Value)}).ToList()
					}
				);
			}
		}

		/// <summary>
		///     AddChild
		/// </summary>
		/// <param name="product"></param>
		/// <exception cref="InvalidConfigurableProductException"></exception>
		/// <exception cref="InvalidChildProductException"></exception>
		public void AddChild(Product product)
		{
			if (!_options.Any())
			{
				throw new InvalidConfigurableProductException(
					"No configurable options have been defined for this product");
			}

			if (this.Children.Any(çhild => çhild.Sku == product.Sku))
			{
				throw new InvalidChildProductException("The product is already attached.");
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

			_addedChildren.Add(product.Sku);
		}


		public void AddChild(string sku)
		{
			var product = _context.Products.GetProductBySku(sku);

			AddChild(product);
		}

		public void RemoveChild(string sku)
		{
			_removedChildren.Add(sku);
		}
	}
}