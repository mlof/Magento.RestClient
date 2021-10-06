using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validators;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Configuration;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Exceptions.Authentication;
using Magento.RestClient.Exceptions.Generic;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Context
{
	public class MagentoAdminContext : BaseContext, IAdminContext
	{
		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="options"></param>
		/// <param name="cache"></param>
		/// <param name="memoryCacheOptions"></param>
		/// <exception cref="InvalidAuthenticationMethodException"></exception>
		public MagentoAdminContext(MagentoClientOptions options, IMemoryCache cache = null,
			MemoryCacheOptions memoryCacheOptions = null) : base(cache, memoryCacheOptions)
		{
			if (options.AuthenticationMethod == AuthenticationMethod.Integration)
			{
				this.Client = MagentoRestClientFactory.CreateIntegrationClient(options.Host, options.ConsumerKey,
					options.ConsumerSecret, options.AccessToken, options.AccessTokenSecret, options.DefaultScope);
			}
			else if (options.AuthenticationMethod == AuthenticationMethod.Admin)

			{
				this.Client = MagentoRestClientFactory.CreateAdminClient(options.Host, options.Username,
					options.Password, options.DefaultScope);
			}
			else
			{
				throw new InvalidAuthenticationMethodException();
			}
		}

		public MagentoAdminContext(string host,
			string consumerKey,
			string consumerSecret,
			string accessToken,
			string accessTokenSecret,
			IMemoryCache cache = null, MemoryCacheOptions memoryCacheOptions = null,
			string defaultScope = "default") : this(
			new MagentoClientOptions(host, consumerKey, accessTokenSecret, accessToken, consumerSecret, defaultScope),
			cache,
			memoryCacheOptions)
		{
		}

		public MagentoAdminContext(string host,
			string username,
			string password,
			IMemoryCache cache = null, MemoryCacheOptions memoryCacheOptions = null,
			string defaultScope = "default") : this(
			new MagentoClientOptions(host, username, password, AuthenticationMethod.Admin, defaultScope), cache,
			memoryCacheOptions)
		{
		}

		public IBulkRepository Bulk => new BulkRepository(this);
		public ISpecialPriceRepository SpecialPrices => new SpecialPriceRepository(this);
		public IProductAttributeGroupRepository ProductAttributeGroups => new ProductAttributeGroupRepository(this);
		public IStoreRepository Stores => new StoreRepository(this);
		public IProductRepository Products => new ProductRepository(this);
		public IProductMediaRepository ProductMedia => new ProductMediaRepository(this);
		public IConfigurableProductRepository ConfigurableProducts => new ConfigurableProductRepository(this);
		public IOrderRepository Orders => new OrderRepository(this);
		public ICustomerRepository Customers => new CustomerRepository(this);
		public ICustomerGroupRepository CustomerGroups => new CustomerGroupRepository(this);
		public IDirectoryRepository Directory => new DirectoryRepository(this);
		public IAttributeSetRepository AttributeSets => new AttributeSetRepository(this);
		public IInvoiceRepository Invoices => new InvoiceRepository(this);
		public ICategoryRepository Categories => new CategoryRepository(this);
		public ICartRepository Carts => new CartRepository(this);
		public IAttributeRepository Attributes => new AttributeRepository(this);
		public IShipmentRepository Shipments => new ShipmentRepository(this);

		/// <inheritdoc cref="ICanGetModules" />
		public async Task<List<string>> GetModules()
		{
			var request = new RestRequest("modules");

			var response = await this.Client.ExecuteAsync<List<string>>(request).ConfigureAwait(false);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}

		public override IRestClient Client { get; }
	};
}