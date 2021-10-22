using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IInventoryStockRepository : IHasQueryable<InventoryStock>
	{
		void Create(InventoryStock stock);
	}
}