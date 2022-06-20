using Magento.RestClient.Configuration;
using Magento.RestClient.Modules.AsynchronousOperations.Abstractions;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Customers.Abstractions;
using Magento.RestClient.Modules.Directory.Abstractions;
using Magento.RestClient.Modules.EAV.Abstractions;
using Magento.RestClient.Modules.Inventory.Abstractions;
using Magento.RestClient.Modules.Order.Abstractions;
using Magento.RestClient.Modules.Quote.Abstractions;
using Magento.RestClient.Modules.Store.Abstractions;
using Magento.RestClient.Modules.System.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Magento.RestClient.Abstractions
{
	public interface IMagentoContext
	{
		RestSharp.RestClient RestClient { get; }
		IMemoryCache Cache { get; }
		ILogger Logger { get; }
		MagentoClientOptions Options { get; }

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
		IProductAttributeRepository ProductAttributes { get; }
		IProductAttributeAsyncRepository ProductAttributeAsync { get; }
	}
}