using Magento.RestClient.Abstractions;

namespace Magento.RestClient.Context
{
	public interface IInventoryStockRepository : IHasQueryable<InventoryStock>
	{
		void Create(InventoryStock stock);
	}
}