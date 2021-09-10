using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient
{
	internal class AdminContext : IAdminContext
	{
		private readonly IRestClient _client;

		public AdminContext(IRestClient client)
		{
			_client = client;
		}


		public IBulkRepository Bulk => new BulkRepository(_client);
		public IProductAttributeGroupRepository ProductAttributeGroups => new ProductAttributeGroupRepository(_client);
		public IStoreRepository Stores => new StoreRepository(_client);
		public IProductRepository Products => new ProductRepository(_client);
		public IProductMediaRepository ProductMedia => new ProductMediaRepository(_client);
		public IConfigurableProductRepository ConfigurableProducts => new ConfigurableProductRepository(_client);
		public IOrderRepository Orders => new OrderRepository(_client);
		public ICustomerRepository Customers => new CustomerRepository(_client);
		public ICustomerGroupRepository CustomerGroups => new CustomerGroupRepository(_client);
		public IDirectoryRepository Directory => new DirectoryRepository(_client);
		public IAttributeSetRepository AttributeSets => new AttributeSetRepository(_client);
		public IInvoiceRepository Invoices => new InvoiceRepository(_client);
		public ICategoryRepository Categories => new CategoryRepository(_client);
		public ICartRepository Carts => new CartRepository(_client);
		public IAttributeRepository Attributes => new AttributeRepository(_client);
		public IShipmentRepository Shipments => new ShipmentRepository(_client);

		/// <inheritdoc cref="ICanGetModules" />
		public List<string> GetModules()
		{
			var request = new RestRequest("modules");


			var response = _client.Execute<List<string>>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}
	}

	internal class ProductAttributeGroupRepository : IProductAttributeGroupRepository
	{
		private readonly IRestClient _client;

		public ProductAttributeGroupRepository(IRestClient client)
		{
			_client = client;
		}

		private IQueryable<AttributeGroup> _productAttributeGroupRepositoryImplementation =>
			new MagentoQueryable<AttributeGroup>(_client, "products/attribute-sets/groups/list");

		public IEnumerator<AttributeGroup> GetEnumerator()
		{
			return this._productAttributeGroupRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) this._productAttributeGroupRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => this._productAttributeGroupRepositoryImplementation.ElementType;

		public Expression Expression => this._productAttributeGroupRepositoryImplementation.Expression;

		public IQueryProvider Provider => this._productAttributeGroupRepositoryImplementation.Provider;
	}
}