using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Inventory.Models;

namespace Magento.RestClient.Modules.Inventory.Abstractions
{
	public interface IInventorySourceItemRepository : IHasQueryable<InventorySourceItem>
	{
		Task Create(params InventorySourceItem[] items);
		Task Delete(params InventorySourceItem[] items);
	}
}