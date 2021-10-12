using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IInventoryStockRepository : IHasQueryable<InventoryStock>
	{
		void Create(InventoryStock stock);
	}
}