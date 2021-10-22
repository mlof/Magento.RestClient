using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Inventory;

namespace Magento.RestClient.Data.Repositories
{
	public class InventorySourceItemRepository : AbstractRepository, IInventorySourceItemRepository
	{
		public InventorySourceItemRepository(IContext context) : base(context)
		{
		}


		public IQueryable<InventorySourceItem> AsQueryable()
		{
			throw new System.NotImplementedException();
		}

		public void Create(params InventorySourceItem[] items)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(params InventorySourceItem[] items)
		{
			throw new System.NotImplementedException();
		}
	}
}