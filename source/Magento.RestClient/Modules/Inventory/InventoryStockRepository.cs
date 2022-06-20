using System;
using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Inventory.Abstractions;
using Magento.RestClient.Modules.Inventory.Models;

namespace Magento.RestClient.Modules.Inventory
{
	public class InventoryStockRepository : AbstractRepository, IInventoryStockRepository
	{
		public InventoryStockRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public IQueryable<InventoryStock> AsQueryable()
		{
			throw new NotImplementedException();
		}

		public void Create(InventoryStock stock)
		{
			throw new NotImplementedException();
		}
	}
}