using System;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.AsynchronousOperations.Models;

namespace Magento.RestClient.Modules.AsynchronousOperations.Abstractions
{
	public interface IAsyncRepository : IHasQueryable<BulkOperation>
	{
		Task<BulkOperation> GetStatus(Guid uuid);

		Task<BulkOperation> AwaitBulkOperations(Guid uuid, TimeSpan? delay = null);
	}
}