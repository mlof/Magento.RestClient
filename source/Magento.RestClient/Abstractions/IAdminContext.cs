using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Context;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions.Generic;
using RestSharp;

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

	public interface IModuleRepository
	{
		Task<List<string>> GetModules();
	}

	public class ModuleRepository : AbstractRepository, IModuleRepository
	{
		public ModuleRepository(IContext context) : base(context)
		{
		}

		/// <inheritdoc cref="ICanGetModules" />
		public async Task<List<string>> GetModules()
		{
			var request = new RestRequest("modules");

			var response = await this.Client.ExecuteAsync<List<string>>(request).ConfigureAwait(false);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}
	}
}