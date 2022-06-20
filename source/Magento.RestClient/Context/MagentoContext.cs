using System;
using System.Collections.Generic;
using System.Globalization;
using Ardalis.GuardClauses;
using JsonExts.JsonPath;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Authentication;
using Magento.RestClient.Configuration;
using Magento.RestClient.Exceptions.Authentication;
using Magento.RestClient.Modules.AsynchronousOperations;
using Magento.RestClient.Modules.AsynchronousOperations.Abstractions;
using Magento.RestClient.Modules.Catalog;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Customers;
using Magento.RestClient.Modules.Customers.Abstractions;
using Magento.RestClient.Modules.Directory;
using Magento.RestClient.Modules.Directory.Abstractions;
using Magento.RestClient.Modules.EAV;
using Magento.RestClient.Modules.EAV.Abstractions;
using Magento.RestClient.Modules.Inventory;
using Magento.RestClient.Modules.Inventory.Abstractions;
using Magento.RestClient.Modules.Order;
using Magento.RestClient.Modules.Order.Abstractions;
using Magento.RestClient.Modules.Quote;
using Magento.RestClient.Modules.Quote.Abstractions;
using Magento.RestClient.Modules.Store;
using Magento.RestClient.Modules.Store.Abstractions;
using Magento.RestClient.Modules.System;
using Magento.RestClient.Modules.System.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using RestSharp.Serializers.NewtonsoftJson;
using Serilog;

namespace Magento.RestClient.Context
{
	public class MagentoContext : IMagentoContext
	{
		public MagentoContext(MagentoClientOptions options, IMemoryCache cache = null,
			MemoryCacheOptions memoryCacheOptions = null)
		{
			this.Options = options;

			if (cache != null)
			{
				this.Cache = cache;
			}
			else
			{
				if (memoryCacheOptions == null)
				{
					memoryCacheOptions = new MemoryCacheOptions();
				}

				this.Cache = new MemoryCache(memoryCacheOptions);
			}

			Guard.Against.NullOrWhiteSpace(Options.Host);
			var host = Options.Host;
			if (host.EndsWith("/"))
			{
				host = host.TrimEnd('/');
			}

			if (string.IsNullOrWhiteSpace(Options.DefaultScope))
			{
				RestClient = new RestSharp.RestClient($"{host}/rest/V1/");
			}
			else
			{
				RestClient = new RestSharp.RestClient($"{host}/rest/{{scope}}/V1/");

				RestClient.AddDefaultUrlSegment("scope", Options.DefaultScope);
			}


			RestClient.UseNewtonsoftJson(JsonSerializerSettings);

			switch (Options.AuthenticationMethod)
			{
				case AuthenticationMethod.Integration:
					Guard.Against.NullOrWhiteSpace(options.ConsumerKey);
					Guard.Against.NullOrWhiteSpace(options.ConsumerSecret);
					Guard.Against.NullOrWhiteSpace(options.AccessToken);
					Guard.Against.NullOrWhiteSpace(options.AccessTokenSecret);
					RestClient.Authenticator = OAuth1Authenticator.ForProtectedResource(options.ConsumerKey,
						options.ConsumerSecret, options.AccessToken,
						options.AccessTokenSecret, OAuthSignatureMethod.HmacSha256);
					break;
				case AuthenticationMethod.Admin:

					var customerTokenUrl = options.Host + "/rest/V1/integration/customer/token";
					Guard.Against.NullOrWhiteSpace(options.Username);
					Guard.Against.NullOrWhiteSpace(options.Password);
					RestClient.Authenticator =
						new MagentoUserAuthenticator(customerTokenUrl, options.Username, options.Password, 1);

					break;
				case AuthenticationMethod.Customer:
				case AuthenticationMethod.Guest:
				default:
					throw new NotImplementedException(
						"Customer and Guest logins have not been implemented at this time.");
			}
		}

		private readonly static JsonSerializerSettings JsonSerializerSettings = new() {
			NullValueHandling = NullValueHandling.Ignore,
			Culture = CultureInfo.InvariantCulture,
			Formatting = Formatting.Indented,
			DateFormatString = "yyyy-MM-dd hh:mm:ss",
			DefaultValueHandling = DefaultValueHandling.Ignore,
			Converters = new List<JsonConverter> {
				//new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal},
				new JsonPathObjectConverter()
			}
		};

		public RestSharp.RestClient RestClient { get; }
		public IMemoryCache Cache { get; }
		public ILogger Logger => Log.Logger;
		public MagentoClientOptions Options { get; }

		public IProductAttributeRepository ProductAttributes => new ProductAttributeRepository(this);
		public IProductAttributeAsyncRepository ProductAttributeAsync => new ProductAttributeAsyncRepository(this);

		public IAsyncRepository Async => new AsyncRepository(this);
		public ISpecialPriceRepository SpecialPrices => new SpecialPriceRepository(this);
		public IModuleRepository Modules => new ModuleRepository(this);
		public IProductAttributeGroupRepository ProductAttributeGroups => new ProductAttributeGroupRepository(this);
		public IStoreRepository Stores => new StoreRepository(this);
		public IProductRepository Products => new ProductRepository(this);

		/// <inheritdoc />
		public IProductAsyncRepository ProductsAsync => new ProductAsyncRepository(this);

		public IProductMediaRepository ProductMedia => new ProductMediaRepository(this);
		public IConfigurableProductRepository ConfigurableProducts => new ConfigurableProductRepository(this);
		public IOrderRepository Orders => new OrderRepository(this);
		public ICustomerRepository Customers => new CustomerRepository(this);
		public ICustomerGroupRepository CustomerGroups => new CustomerGroupRepository(this);
		public IDirectoryRepository Directory => new DirectoryRepository(this);
		public IAttributeSetRepository AttributeSets => new AttributeSetRepository(this);
		public IInvoiceRepository Invoices => new InvoiceRepository(this);
		public ICategoryRepository Categories => new CategoryRepository(this);
		public IInventoryStockRepository InventoryStocks => new InventoryStockRepository(this);
		public IInventorySourceItemRepository InventorySourceItems => new InventorySourceItemRepository(this);
		public IInventorySourceRepository InventorySources => new InventorySourceRepository(this);
		public ICartRepository Carts => new CartRepository(this);
		public IAttributeRepository Attributes => new AttributeRepository(this);
		public IShipmentRepository Shipments => new ShipmentRepository(this);
	}
}