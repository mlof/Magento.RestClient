namespace Magento.RestClient.Data.Repositories
{
	public interface IBulkRepository
	{
		BulkActionStatus GetStatus(string uuid);
	}
}