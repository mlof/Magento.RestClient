using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Repositories.Abstractions;

namespace Magento.RestClient.Context
{
	public class InventorySourceRepository : AbstractRepository, IInventorySourceRepository
	{
		public InventorySourceRepository(IContext context) : base(context)
		{
		}

		public IQueryable<InventorySource> AsQueryable()
		{
			throw new System.NotImplementedException();
		}

		public void Create(InventorySource source)
		{
			throw new System.NotImplementedException();
		}

		public InventorySource GetByCode(string sourceCode)
		{
			throw new System.NotImplementedException();
		}

		public void Update(string sourceCode, InventorySource source)
		{
			throw new System.NotImplementedException();
		}
	}
}