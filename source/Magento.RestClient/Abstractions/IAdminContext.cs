using Magento.RestClient.Context;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;

namespace Magento.RestClient.Abstractions
{
	public interface IAdminContext :  IContext
	{
		IProductAttributeGroupRepository ProductAttributeGroups { get; }
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
		IInventoryStockRepository InventoryStocks { get; }
		IInventorySourceItemRepository InventorySourceItems { get; }
		InventorySourceRepository InventorySources { get; }
		ICartRepository Carts { get; }
		IAttributeRepository Attributes { get; }
		IShipmentRepository Shipments { get; }
		IBulkRepository Bulk { get; }
		ISpecialPriceRepository SpecialPrices { get; }
		IModuleRepository Modules { get; }
	}
}