namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IWriteOrderRepository
	{
		void Cancel(long orderId);
		void Hold(long orderId);
		void Unhold(long orderId);
		void Refund(long orderId);
		void Ship(long orderId);
	}
}