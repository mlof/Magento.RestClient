using System;
using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Validators;
using Magento.RestClient.Extensions;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Models.Stock;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class ProductModel : IDomainModel
	{
		private readonly IAdminContext _context;
		private readonly ProductValidator _productValidator;

		public ProductModel(IAdminContext context, string sku)
		{
			_context = context;
			this.Sku = sku;
			_productValidator = new ProductValidator();
			this.Scope = "all";
			Refresh();
		}

		public string Sku { get; }


		public string Scope { get; }

		public string Name {
			get;
			set;
		}

		public long AttributeSetId { get; set; }

		public ProductVisibility Visibility { get; set; }

		public List<CustomAttribute> CustomAttributes { get; set; }

		public decimal? Price { get; set; }

		public StockItem StockItem { get; set; }

		public bool IsPersisted { get; set; }

		public void Refresh()
		{
			var existingProduct = _context.Products.GetProductBySku(this.Sku, this.Scope);
			if (existingProduct == null)
			{
				this.AttributeSetId = _context.Search.GetDefaultAttributeSet(EntityType.CatalogProduct).AttributeSetId
					.GetValueOrDefault();
			}
			else
			{
				this.Name = existingProduct.Name;
				this.Price = existingProduct.Price;
				this.AttributeSetId = existingProduct.AttributeSetId;
				this.Visibility = Enum.Parse<ProductVisibility>(existingProduct.Visibility.ToString());
				this.CustomAttributes = existingProduct.CustomAttributes;
				this.IsPersisted = true;
				this.Type = existingProduct.TypeId;

				if (this.Type == ProductType.Configurable)
				{
					this.Children = this._context.ConfigurableProducts.GetConfigurableChildren(Sku);
				}
			}
		}

		public List<Product> Children { get; set; }

		public ProductType Type { get; set; }

		public void Save()
		{
			var existingProduct = _context.Products.GetProductBySku(this.Sku);

			var product = new Product {
				Sku = this.Sku,
				Name = this.Name,
				Price = this.Price,
				AttributeSetId = this.AttributeSetId,
				Visibility = (long) this.Visibility,
				TypeId = Type
			};
			if (this.StockItem != null)
			{
				product.SetStockItem(this.StockItem);
			}

			if (existingProduct == null)
			{
				_context.Products.CreateProduct(product);

				this.IsPersisted = true;
			}
			else
			{
				_context.Products.UpdateProduct(this.Sku, product, scope: this.Scope);
			}

			Refresh();
		}


		public void Delete()
		{
			_context.Products.DeleteProduct(this.Sku);
		}


		public ProductModel SetAttribute(string attributeCode, string value)
		{
			if (this.CustomAttributes.Any(attribute => attribute.AttributeCode == attributeCode))
			{
				this.CustomAttributes.Single(attribute => attribute.AttributeCode == attributeCode).Value =
					value;
			}
			else
			{
				this.CustomAttributes.Add(new CustomAttribute(attributeCode, value));
			}

			return this;
		}

		public void SetStock(long quantity)
		{
			this.StockItem = new StockItem {IsInStock = quantity > 0, Qty = quantity};
		}
	}
}