namespace Magento.RestClient.Repositories
{
	public interface IBulkRepository
	{
		BulkActionStatus GetStatus(string uuid);
	}
}