using Magento.RestClient.Models.Orders;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface IReadOrderRepository
	{
		Order CreateOrder(Order order);
		Order GetByOrderId(long orderId);
	}
}