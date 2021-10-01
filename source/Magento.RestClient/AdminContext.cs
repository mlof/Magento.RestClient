using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient
{
	internal class AdminContext : IAdminContext
	{
		public AdminContext(MagentoClient client)
		{
			this.Client = client._client;
			this.Cache = client.cache;
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


			var response = await this.Client.ExecuteAsync<List<string>>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}

		public IRestClient Client { get; }
		public IMemoryCache Cache { get; }
	};
}