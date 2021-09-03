namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IOrderRepository : IReadOrderRepository, IWriteOrderRepository
	{
		void CreateInvoice(long orderId);
	}
}