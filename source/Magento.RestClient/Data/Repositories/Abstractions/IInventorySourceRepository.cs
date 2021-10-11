using Magento.RestClient.Abstractions;

namespace Magento.RestClient.Context
{
	public interface IInventorySourceRepository : IHasQueryable<InventorySource>
	{
		void Create(InventorySource source);
		InventorySource GetByCode(string sourceCode);
		void Update(string sourceCode, InventorySource source);

	}
}