using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IInventorySourceRepository : IHasQueryable<InventorySource>
	{
		void Create(InventorySource source);
		InventorySource GetByCode(string sourceCode);
		void Update(string sourceCode, InventorySource source);

	}
}