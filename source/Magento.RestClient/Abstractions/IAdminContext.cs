using Magento.RestClient.Data.Repositories.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Abstractions
{
	public interface IAdminContext : ICanGetModules, IContext
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
		ICartRepository Carts { get; }
		IAttributeRepository Attributes { get; }
		IShipmentRepository Shipments { get; }
		IBulkRepository Bulk { get; }
		ISpecialPriceRepository SpecialPrices { get; }
	}

	public interface IContext 
	{
		IRestClient Client { get;  }
		IMemoryCache Cache { get;  }
	}
}