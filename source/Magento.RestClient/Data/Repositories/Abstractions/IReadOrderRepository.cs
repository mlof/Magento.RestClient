using Magento.RestClient.Data.Models.Orders;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IReadOrderRepository
	{
		Order CreateOrder(Order order);
		Order GetByOrderId(long orderId);
	}
}