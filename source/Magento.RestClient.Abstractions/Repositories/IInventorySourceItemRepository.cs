using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IInventorySourceItemRepository : IHasQueryable<InventorySourceItem>
	{

		Task Create(params InventorySourceItem[] items);
		Task Delete(params InventorySourceItem[] items);
	}
}