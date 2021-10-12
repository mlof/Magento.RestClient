using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IInventorySourceItemRepository : IHasQueryable<InventorySourceItem>
	{

		void Create(params InventorySourceItem[] items);
		void Delete(params InventorySourceItem[] items);
	}
}