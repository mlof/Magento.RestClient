using System;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IBulkRepository : IHasQueryable<BulkOperation>
	{
		Task<BulkOperation> GetStatus(Guid uuid);

		Task<BulkOperation> Await(Guid uuid, TimeSpan? delay = null);
	}
}