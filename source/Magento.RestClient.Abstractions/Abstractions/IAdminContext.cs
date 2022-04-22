using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Abstractions.Abstractions
{
	public interface IAdminContext :  IContext
	{
		IProductAttributeGroupRepository ProductAttributeGroups { get; }
		IStoreRepository Stores { get; }
		IProductRepository Products { get; }
		IProductAsyncRepository ProductsAsync { get; }
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
		IInventorySourceRepository InventorySources { get; }
		ICartRepository Carts { get; }
		IAttributeRepository Attributes { get; }
		IShipmentRepository Shipments { get; }
		IAsyncRepository Async { get; }
		ISpecialPriceRepository SpecialPrices { get; }
		IModuleRepository Modules { get; }
	}



}