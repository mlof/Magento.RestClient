using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Stock;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Models.EAV;
using Magento.RestClient.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public class ProductModel : IProductModel, IDomainModel
	{
		protected readonly IAdminContext Context;
		private List<SpecialPrice> _specialPrices;

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sku"></param>
		/// <exception cref="ProductSkuInvalidException"></exception>
		public ProductModel(IAdminContext context, string sku)
		{
			this.Validator = new ProductModelValidator();
			if (!Regex.Match(sku, "^[\\w-]*$").Success)
			{
				throw new ProductSkuInvalidException(sku);
			}

			Context = context;
			this.Sku = sku;
			this.Scope = "all";
			Log.Information("Initializing {Sku}", sku);

			Refresh().GetAwaiter().GetResult();
		}

		public string Sku { get; }

		[JsonIgnore] public string Scope { get; }

		public string Name {
			get;
			set;
		}

		public long AttributeSetId { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public ProductVisibility Visibility { get; set; }

		public List<CustomAttribute> CustomAttributes { get; set; }

		public decimal? Price { get; set; }

		public StockItem StockItem { get; set; }

		public ProductType Type { get; set; }

		public dynamic this[string attributeCode] {
			get => GetAttribute(attributeCode);
			set => SetAttribute(attributeCode, value).GetAwaiter().GetResult();
		}

		[JsonIgnore] public IValidator Validator { get; }
		[JsonIgnore] public bool IsPersisted { get; set; }

		public string UrlKey {
			get => this["url_key"];
			set => SetAttribute("url_key", value).GetAwaiter().GetResult();
		}

		public string Description {
			get => this["description"];
			set => SetAttribute("description", value).GetAwaiter().GetResult();
		}

		public string ShortDescription {
			get => this["short_description"];
			set => SetAttribute("short_description", value).GetAwaiter().GetResult();
		}


		public async virtual Task Refresh()
		{
			var sw = Stopwatch.StartNew();
			var existingProduct = await Context.Products.GetProductBySku(this.Sku, this.Scope).ConfigureAwait(false);


			if (existingProduct == null)
			{
				this.AttributeSetId = Context.AttributeSets.GetDefaultAttributeSet().AttributeSetId
					.GetValueOrDefault();
				this.Name = this.Sku;
				this.CustomAttributes = new List<CustomAttribute>();
				_specialPrices = new List<SpecialPrice>();
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
				_specialPrices =
					await Context.SpecialPrices.GetSpecialPrices(this.Sku).ConfigureAwait(false) ??
					new List<SpecialPrice>();

				if (existingProduct.MediaGalleryEntries != null)
				{
					this.MediaEntries = existingProduct.MediaGalleryEntries;
				}
			}

			sw.Stop();
			Log.Debug("Refreshed {Sku} in {Elapsed}", this.Sku, sw.Elapsed);
		}

		public List<MediaEntry> MediaEntries { get; set; } = new();

		public void AddMediaEntryByFilename(string fileName, FileInfo fileInfo)
		{
			if (this.MediaEntries.All(media => media.Label != fileName))
			{
				MediaEntries.Add(new MediaEntry() {
					Label = fileName,
					Disabled = false,
					MediaType = ProductMediaType.Image,
					Content = new ProductMediaContent(fileInfo)
				});
			}
		}

		public async virtual Task SaveAsync()
		{
			var sw = Stopwatch.StartNew();
			var existingProduct = Context.Products.GetProductBySku(this.Sku);

			var product = GetProduct();

			if (existingProduct == null)
			{
				Log.Information("Creating {Sku}", this.Sku);

				await Context.Products.CreateProduct(product).ConfigureAwait(false);

				this.IsPersisted = true;
			}
			else
			{
				Log.Information("Updating {Sku}", this.Sku);

				await Context.Products.UpdateProduct(this.Sku, product, scope: this.Scope).ConfigureAwait(false);
			}

			foreach (var specialPrice in this.SpecialPrices)
			{
				await Context.SpecialPrices.AddOrUpdateSpecialPrices(specialPrice).ConfigureAwait(false);
			}

			foreach (var item in MediaEntries)
			{
				if (item.Id == null)
				{
					await Context.ProductMedia.Create(Sku, item);
				}
			}


			sw.Stop();
			Log.Debug("Saved {Sku} in {Elapsed}", this.Sku, sw.Elapsed);
		}


		public Product GetProduct()
		{
			var product = new Product {
				Sku = this.Sku,
				Name = this.Name,
				Price = this.Price,
				AttributeSetId = this.AttributeSetId,
				Visibility = (long)this.Visibility,
				CustomAttributes = this.CustomAttributes,
				TypeId = this.Type,
			};
			if (this.StockItem != null)
			{
				product.SetStockItem(this.StockItem);
			}

			return product;
		}

		public Task Delete()
		{
			return Context.Products.DeleteProduct(this.Sku);
		}

		public void SetStock(long quantity)
		{
			this.StockItem = new StockItem { IsInStock = quantity > 0, Qty = quantity };
		}

		public ConfigurableProductModel GetConfigurableProductModel()
		{
			return new(Context, this.Sku);
		}


		private dynamic GetAttribute(string attributeCode)
		{
			return this.CustomAttributes.SingleOrDefault(attribute => attribute.AttributeCode == attributeCode)?.Value;
		}

		private async Task<ProductModel> SetAttribute(string attributeCode, dynamic inputValue)
		{
			// validateValue 
			var attribute = await Context.Attributes.GetByCode(attributeCode).ConfigureAwait(false);

			dynamic value = null;
			if (attribute.Options.Count > 0 && !string.IsNullOrWhiteSpace(inputValue as string))
			{
				var option = attribute.Options.SingleOrDefault(option =>
					option.Label.Equals(inputValue as string, StringComparison.InvariantCultureIgnoreCase));

				if (option == null)
				{
					throw new InvalidCustomAttributeException(attributeCode, attribute.Options, inputValue);
				}
				else
				{
					value = option.Value;
				}
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


		public void SetSpecialPrice(DateTime start, DateTime end, decimal price, long storeId = 0)
		{
			_specialPrices.Add(new SpecialPrice() {
				PriceFrom = start,
				PriceTo = end,
				Sku = this.Sku,
				Price = price,
				StoreId = storeId
			});
		}

		public IReadOnlyCollection<SpecialPrice> SpecialPrices => _specialPrices.AsReadOnly();

		public CustomAttribute? GetAttributeById(long optionAttributeId)
		{
			var code = Context.Attributes.GetById(optionAttributeId);
			return CustomAttributes.SingleOrDefault(attribute => attribute.AttributeCode == attribute.AttributeCode);
		}
	}
}