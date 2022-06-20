using System.Threading.Tasks;
using Magento.RestClient.Modules.AsynchronousOperations.Abstractions;
using Magento.RestClient.Modules.AsynchronousOperations.Models;

namespace Magento.RestClient.Modules.AsynchronousOperations
{
	public static class AsyncRepositoryExtensions
	{
		public static Task<BulkOperation> AwaitBulkOperations(this IAsyncRepository repository,
			BulkActionResponse response)
		{
			return repository.AwaitBulkOperations(response.BulkUuid);
		}
	}
}