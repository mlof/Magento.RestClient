using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Inventory.Models;

namespace Magento.RestClient.Modules.Inventory.Abstractions
{
	public interface IInventoryStockRepository : IHasQueryable<InventoryStock>
	{
		void Create(InventoryStock stock);
	}
}