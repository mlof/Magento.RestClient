using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Bulk;

namespace Magento.RestClient.Abstractions.Repositories
{
	public static class BulkRepositoryExtensions
	{
		public static Task<BulkOperation> AwaitBulkOperations(this IAsyncRepository repository,
			BulkActionResponse response)
		{
			return repository.AwaitBulkOperations(response.BulkUuid);
		}
	}
}