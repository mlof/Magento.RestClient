using Magento.RestClient.Abstractions;

namespace Magento.RestClient.Context
{
	public interface IInventorySourceItemRepository : IHasQueryable<InventorySourceItem>
	{

		void Create(params InventorySourceItem[] items);
		void Delete(params InventorySourceItem[] items);
	}
}