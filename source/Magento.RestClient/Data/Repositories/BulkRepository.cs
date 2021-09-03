using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class BulkRepository : AbstractRepository, IBulkRepository
	{
		public BulkRepository(IRestClient client)
		{
		}

		public BulkActionStatus GetStatus(string uuid)
		{
			return new();
		}
	}
}