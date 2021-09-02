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
		private readonly IAdminClient _client;
		private readonly ProductValidator _productValidator;

		public ProductModel(IAdminClient client, string sku)
		{
			_client = client;
			this.Sku = sku;
			this._productValidator = new ProductValidator();
			this.Scope = "all";
			Refresh();
		}

		public string Sku { get; private set; }


		public string Scope { get; private set; }

		public string Name {
			get;
			set;
		}


		public void Delete()
		{
			_client.Products.DeleteProduct(this.Sku);
		}


		public ProductModel SetAttribute(string attributeCode, string value)
		{
			if (CustomAttributes.Any(attribute => attribute.AttributeCode == attributeCode))
			{
				CustomAttributes.Single(attribute => attribute.AttributeCode == attributeCode).Value =
					value;
			}
			else
			{
				CustomAttributes.Add(new CustomAttribute(attributeCode, value));
			}

			return this;
		}

		public bool IsPersisted { get; set; }

		public void Refresh()
		{
			var existingProduct = _client.Products.GetProductBySku(Sku, this.Scope);
			if (existingProduct == null)
			{
			}
			else
			{
				this.Name = existingProduct.Name;
				this.Price = existingProduct.Price;
				this.AttributeSetId = existingProduct.AttributeSetId;
				this.Visibility = Enum.Parse<ProductVisibility>(existingProduct.Visibility.ToString());
				this.CustomAttributes = existingProduct.CustomAttributes;
				this.IsPersisted = true;
			}
		}

		public long AttributeSetId { get; set; }

		public ProductVisibility Visibility { get; set; }

		public List<CustomAttribute> CustomAttributes { get; set; }

		public decimal? Price { get; set; }

		public void Save()
		{
			var existingProduct = _client.Products.GetProductBySku(this.Sku);

			var product = new Product() {
				Sku = this.Sku, Name = this.Name, Price = this.Price, Visibility = (long) Visibility
			};
			if (this.StockItem != null)
			{
				product.SetStockItem(this.StockItem);
			}

			if (existingProduct == null)
			{
				_client.Products.CreateProduct(product, true);

				this.IsPersisted = true;
			}
			else
			{
				_client.Products.UpdateProduct(this.Sku, product, scope: this.Scope);
			}

			Refresh();
		}

		public void SetStock(long quantity)
		{
			this.StockItem = new StockItem {IsInStock = quantity > 0, Qty = quantity};
		}

		public StockItem StockItem { get; set; }
	}
}