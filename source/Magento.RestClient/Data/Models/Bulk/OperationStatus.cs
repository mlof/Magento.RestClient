namespace Magento.RestClient.Data.Models.Bulk
{
	public enum OperationStatus
	{
		Complete = 1,
		OperationFailedTryAgain = 2,
		OperationFailed = 3,
		Open = 4,
		Rejected = 5
	}
}