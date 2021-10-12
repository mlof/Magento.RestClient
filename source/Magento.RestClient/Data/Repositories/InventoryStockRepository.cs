using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Inventory;
using Magento.RestClient.Data.Repositories.Abstractions;

namespace Magento.RestClient.Data.Repositories
{
	public class InventoryStockRepository : AbstractRepository, IInventoryStockRepository
	{
		public InventoryStockRepository(IContext context) : base(context)
		{
		}

		public IQueryable<InventoryStock> AsQueryable()
		{
			throw new System.NotImplementedException();
		}

		public void Create(InventoryStock stock)
		{
			throw new System.NotImplementedException();
		}
	}
}