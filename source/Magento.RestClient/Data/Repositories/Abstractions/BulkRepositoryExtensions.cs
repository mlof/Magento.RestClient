using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Bulk;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public static class BulkRepositoryExtensions
	{
		public static Task<BulkOperation> AwaitBulkOperations(this IBulkRepository repository,
			BulkActionResponse response)
		{
			return repository.AwaitBulkOperations(response.BulkUuid);
		}
	}
}