using System.Collections.Generic;
using AgileObjects.AgileMapper;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Repositories;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using Magento.RestClient.Search.Abstractions;
using RestSharp;

namespace Magento.RestClient
{
	internal class IntegrationClient : IIntegrationClient
	{
		private readonly IRestClient _client;

		public IntegrationClient(IRestClient client)
		{
			this.Stores = new StoreRepository(client);
			this.Products = new ProductRepository(client);

			this.Orders = new OrderRepository(client);
			this.Customers = new CustomerRepository(client);
			this.Directory = new DirectoryRepository(client);
			this.CustomerGroups = new CustomerGroupRepository(client);
			this.ConfigurableProducts = new ConfigurableProductRepository(client);
			this.AttributeSets = new AttributeSetRepository(client);
			this.ProductMedia = new ProductMediaRepository(client);
			this.Invoices = new InvoiceRepository(client);
			this.Categories = new CategoryRepository(client);
			this.Carts = new CartRepository(client);
			this.Search = new SearchService(client);
			this.Attributes = new AttributeRepository(client);
			this._client = client;
		}

		public ISearchService Search { get; }
		public IStoreRepository Stores { get; }
		public IProductRepository Products { get; }
		public IProductMediaRepository ProductMedia { get; }
		public IConfigurableProductRepository ConfigurableProducts { get; }
		public IOrderRepository Orders { get; }
		public ICustomerRepository Customers { get; }
		public ICustomerGroupRepository CustomerGroups { get; set; }
		public IDirectoryRepository Directory { get; set; }
		public IAttributeSetRepository AttributeSets { get; }
		public IInvoiceRepository Invoices { get; }
		public ICategoryRepository Categories { get; }
		public ICartRepository Carts { get; }
		public IAttributeRepository Attributes { get; set; }

		///<inheritdoc cref="ICanGetModules"/>
		public List<string> GetModules()
		{
			var request = new RestRequest("modules");


			var response = _client.Execute<List<string>>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}
			else
			{
				throw MagentoException.Parse(response.Content);
			}
		}
	}
}