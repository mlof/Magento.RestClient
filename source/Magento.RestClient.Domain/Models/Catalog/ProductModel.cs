using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using FluentValidation;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Domain;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Models.Catalog.Products.Extensions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Stock;
using Magento.RestClient.Domain.Models.Catalog.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public class ProductModel : IProductModel
	{
		protected readonly IAdminContext Context;
		private List<MediaEntry> _mediaEntries = new List<MediaEntry>();
		private List<SpecialPrice> _specialPrices;

		/// <summary>
		///     ctor
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

		public long AttributeSetId { get; set; }

		[JsonIgnore] public IValidator Validator { get; }

		public string Sku { get; }

		[JsonIgnore] public string Scope { get; }

		public string Name {
			get;
			set;
		}

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

		[JsonIgnore] public bool IsPersisted { get; set; }


		/// <summary>
		/// UrlKey
		/// </summary>
		/// <exception cref="InvalidCustomAttributeValueException">Get. Ignore.</exception>
		/// <exception cref="InvalidCustomAttributeValueException">Set. Ignore.</exception>
		public string UrlKey {
			get => this["url_key"];
			set => SetAttribute("url_key", value).GetAwaiter().GetResult();
		}


		/// <summary>
		/// Description
		/// </summary>
		/// <exception cref="InvalidCustomAttributeValueException">Get. Ignore.</exception>
		/// <exception cref="InvalidCustomAttributeValueException">Set. Ignore.</exception>
		public string Description {
			get => this["description"];
			set => SetAttribute("description", value).GetAwaiter().GetResult();
		}


		/// <summary>
		/// ShortDescription
		/// </summary>
		/// <exception cref="InvalidCustomAttributeValueException">Get. Ignore.</exception>
		/// <exception cref="InvalidCustomAttributeValueException">Set. Ignore.</exception>
		public string ShortDescription {
			get => this["short_description"];
			set => SetAttribute("short_description", value).GetAwaiter().GetResult();
		}


		async virtual public Task Refresh()
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
				Mapper.Map(existingProduct).Over(this);

				this.Visibility = Enum.Parse<ProductVisibility>(existingProduct.Visibility.ToString());
				this.Type = existingProduct.TypeId;
				_specialPrices =
					await Context.SpecialPrices.GetSpecialPrices(this.Sku).ConfigureAwait(false) ??
					new List<SpecialPrice>();

				if (existingProduct.MediaGalleryEntries != null)
				{
					_mediaEntries = existingProduct.MediaGalleryEntries.ToList();
				}
			}

			sw.Stop();
			Log.Debug("Refreshed {Sku} in {Elapsed}", this.Sku, sw.Elapsed);
		}

		public IReadOnlyList<MediaEntry> MediaEntries => _mediaEntries.AsReadOnly();


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

			foreach (var item in this.MediaEntries)
			{
				if (item.Id == null)
				{
					await Context.ProductMedia.Create(this.Sku, item);
				}
			}


			sw.Stop();
			Log.Debug("Saved {Sku} in {Elapsed}", this.Sku, sw.Elapsed);
		}


		public Product GetProduct()
		{
			var product = new Product
			{
				Sku = this.Sku,
				Name = this.Name,
				Price = this.Price,
				AttributeSetId = this.AttributeSetId,
				Visibility = (long)this.Visibility,
				CustomAttributes = this.CustomAttributes,
				TypeId = this.Type,
				Options = this.Options.Select(option => option with { ProductSku = this.Sku }).ToList()
			};
			if (this.StockItem != null)
			{
				product.SetStockItem(this.StockItem);
			}

			return product;
		}

		public List<ProductOption> Options { get; set; } = new List<ProductOption>();

		public Task Delete()
		{
			return Context.Products.DeleteProduct(this.Sku);
		}

		public IReadOnlyCollection<SpecialPrice> SpecialPrices => _specialPrices.AsReadOnly();

		public void AddMediaEntry(MediaEntry entry)
		{
			_mediaEntries.Add(entry);
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

		/// <summary>
		/// SetAttribute
		/// </summary>
		/// <param name="attributeCode"></param>
		/// <param name="inputValue"></param>
		/// <returns></returns>
		/// <exception cref="InvalidCustomAttributeValueException"></exception>
		async private Task<ProductModel> SetAttribute(string attributeCode, dynamic inputValue)
		{
			// validateValue 
			var attribute = await Context.Attributes.GetByCode(attributeCode).ConfigureAwait(false);

			dynamic value = null;
			if (attribute != null && attribute.Options.Count > 0 && !string.IsNullOrWhiteSpace(inputValue as string))
			{
				var option = attribute.Options.SingleOrDefault(option =>
					option.Label.Equals(inputValue as string, StringComparison.InvariantCultureIgnoreCase));

				if (option == null)
				{
					throw new InvalidCustomAttributeValueException(attributeCode, attribute.Options, inputValue);
				}

				value = option.Value;
			}
			else
			{
				value = inputValue;
			}

			if (value != null)
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
			}

			return this;
		}


		public void SetSpecialPrice(DateTime start, DateTime end, decimal price, long storeId = 0)
		{
			_specialPrices.Add(new SpecialPrice
			{
				PriceFrom = start,
				PriceTo = end,
				Sku = this.Sku,
				Price = price,
				StoreId = storeId
			});
		}

		public async Task<CustomAttribute> GetAttributeById(long optionAttributeId)
		{
			var code = await Context.Attributes.GetById(optionAttributeId);
			return this.CustomAttributes.SingleOrDefault(
				attribute => attribute.AttributeCode == code.AttributeCode);
		}
	}
}