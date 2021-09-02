using Magento.RestClient.Search.Abstractions;

namespace Magento.RestClient.Repositories.Abstractions
{

    public interface IAdminClient : ICanGetModules
    {
        ISearchService Search { get; }
        IStoreRepository Stores { get; }
        IProductRepository Products { get; }
        IProductMediaRepository ProductMedia { get; }
        IConfigurableProductRepository ConfigurableProducts { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        ICustomerGroupRepository CustomerGroups { get; }
        IDirectoryRepository Directory { get; }
        IAttributeSetRepository AttributeSets { get; }
        IInvoiceRepository Invoices { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        IAttributeRepository Attributes { get; }
        IShipmentRepository Shipments { get; }
		IBulkRepository Bulk { get; }
	}
}