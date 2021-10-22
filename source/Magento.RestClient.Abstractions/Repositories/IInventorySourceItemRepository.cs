using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IInventorySourceItemRepository : IHasQueryable<InventorySourceItem>
	{

		void Create(params InventorySourceItem[] items);
		void Delete(params InventorySourceItem[] items);
	}
}