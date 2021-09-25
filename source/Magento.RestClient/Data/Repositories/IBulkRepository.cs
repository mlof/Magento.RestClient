using System.Linq;

namespace Magento.RestClient.Data.Repositories
{
	public interface IBulkRepository : IQueryable<BulkOperation>
	{
		BulkActionStatus GetStatus(string uuid);
	}

	public class BulkOperation
	{
	}
}