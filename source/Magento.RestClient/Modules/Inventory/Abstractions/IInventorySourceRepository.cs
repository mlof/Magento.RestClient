using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Inventory.Models;

namespace Magento.RestClient.Modules.Inventory.Abstractions
{
	public interface IInventorySourceRepository : IHasQueryable<InventorySource>
	{
		Task Create(InventorySource source);
		Task<InventorySource> GetByCode(string sourceCode);
		Task Update(string sourceCode, InventorySource source);
	}
}