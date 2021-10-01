using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Models.Stock;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Validators;
using Magento.RestClient.Extensions;

namespace Magento.RestClient.Domain.Models
{
	public class ProductModel : IProductModel, IDomainModel
	{
		protected readonly IAdminContext _context;
		private readonly ProductValidator _productValidator;
		private List<SpecialPrice> _specialPrices;

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sku"></param>
		/// <exception cref="Magento.RestClient.Domain.Models.ProductSkuInvalidException"></exception>
		public ProductModel(IAdminContext context, string sku)
		{
			if (!Regex.Match(sku, "^[\\w-]*$").Success)
			{
				throw new ProductSkuInvalidException(sku);
			}

			_context = context;
			this.Sku = sku;
			_productValidator = new ProductValidator();
			this.Scope = "all";
			Refresh().GetAwaiter().GetResult();
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


		public ProductType Type { get; set; }


		public dynamic this[string attributeCode] {
			get => GetAttribute(attributeCode);
			set => SetAttribute(attributeCode, value).GetAwaiter().GetResult();
		}

		public bool IsPersisted { get; set; }

		public string UrlKey {
			get => this["url_key"];
			set => SetAttribute("url_key", value).GetAwaiter().GetResult();
		}

		public async virtual Task Refresh()
		{
			var existingProduct = await _context.Products.GetProductBySku(this.Sku, this.Scope);

			if (existingProduct == null)
			{
				this.AttributeSetId = _context.AttributeSets.GetDefaultAttributeSet().AttributeSetId
					.GetValueOrDefault();
				this.Name = this.Sku;
				this.CustomAttributes = new List<CustomAttribute>();
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
				this._specialPrices =
					await _context.SpecialPrices.GetSpecialPrices(this.Sku) ?? new List<SpecialPrice>();
			}
		}

		public async virtual Task SaveAsync()
		{
			var existingProduct = _context.Products.GetProductBySku(this.Sku);


			var product = GetProduct();

			if (existingProduct == null)
			{
				await _context.Products.CreateProduct(product);

				this.IsPersisted = true;
			}
			else
			{
				await _context.Products.UpdateProduct(this.Sku, product, scope: this.Scope);
			}

			foreach (var specialPrice in SpecialPrices)
			{
				await _context.SpecialPrices.AddOrUpdateSpecialPrices(specialPrice);
			}
		}


		public Product GetProduct()
		{
			var product = new Product {
				Sku = this.Sku,
				Name = this.Name,
				Price = this.Price,
				AttributeSetId = this.AttributeSetId,
				Visibility = (long) this.Visibility,
				CustomAttributes = this.CustomAttributes,
				TypeId = this.Type,
			};
			if (this.StockItem != null)
			{
				product.SetStockItem(this.StockItem);
			}

			return product;
		}


		public async Task Delete()
		{
			await _context.Products.DeleteProduct(this.Sku);
		}


		public void SetStock(long quantity)
		{
			this.StockItem = new StockItem {IsInStock = quantity > 0, Qty = quantity};
		}

		public ConfigurableProductModel GetConfigurableProductModel()
		{
			return new(_context, this.Sku);
		}

		public ProductGalleryModel GetGalleryModel()
		{
			return new(_context, this.Sku);
		}

		private dynamic GetAttribute(string attributeCode)
		{
			return this.CustomAttributes.SingleOrDefault(attribute => attribute.AttributeCode == attributeCode)?.Value;
		}

		private async Task<ProductModel> SetAttribute(string attributeCode, dynamic inputValue)
		{
			// validateValue 
			var attribute = await _context.Attributes.GetByCode(attributeCode);

			dynamic value;
			if (attribute.Options.Any() && !string.IsNullOrWhiteSpace(inputValue as string))
			{
				var option = attribute.Options.SingleOrDefault(option =>
					option.Label.Equals(inputValue as string, StringComparison.InvariantCultureIgnoreCase));

				value = option != null ? option.Value : inputValue;
			}
			else
			{
				value = inputValue;
			}


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

		public static async Task<BulkActionResponse> SaveBulk(IAdminContext context, List<ProductModel> models)
		{
			return await context.Products.Save(models.Select(model => model.GetProduct()).ToArray());
		}

		public void SetSpecialPrice(DateTime start, DateTime end, decimal price, long storeId = 0)
		{
			this._specialPrices.Add(new SpecialPrice() {
				PriceFrom = start,
				PriceTo = end,
				Sku = this.Sku,
				Price = price,
				StoreId = storeId
			});
		}

		public IReadOnlyCollection<SpecialPrice> SpecialPrices => _specialPrices.AsReadOnly();
	}
}