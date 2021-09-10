using System.Linq;
using Magento.RestClient.Data.Models.Attributes;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IAdminContext : ICanGetModules
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
	}

	public interface IProductAttributeGroupRepository : IQueryable<AttributeGroup>
	{
	}
}