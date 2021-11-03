using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IInventorySourceRepository : IHasQueryable<InventorySource>
	{
		Task Create(InventorySource source);
		Task<InventorySource> GetByCode(string sourceCode);
		Task Update(string sourceCode, InventorySource source);

	}
}