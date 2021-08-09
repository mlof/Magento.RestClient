using Magento.RestClient.Repositories;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient
{
    internal class IntegrationClient : IIntegrationClient
    {
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
            this.Invoices = new InvoiceRepository(client);
            this.Categories = new CategoryRepository(client);
        }

        public IStoreRepository Stores { get; }
        public IProductRepository Products { get; }
        public IConfigurableProductRepository ConfigurableProducts { get; }
        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }
        public ICustomerGroupRepository CustomerGroups { get; set; }
        public IDirectoryRepository Directory { get; set; }
        public IAttributeSetRepository AttributeSets { get; }
        public IInvoiceRepository Invoices { get; }
        public ICategoryRepository Categories { get; }
    }
}