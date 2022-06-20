using System.Threading.Tasks;
using Magento.RestClient.Abstractions;

namespace Magento.RestClient.Modules.Order.Abstractions
{
	public interface IOrderRepository : IHasQueryable<Models.Order>
	{
		Task CreateInvoice(long orderId);
		Task<Models.Order> CreateOrder(Models.Order order);
		Task<Models.Order> GetByOrderId(long orderId);
		Task Cancel(long orderId);
		Task Hold(long orderId);
		Task Unhold(long orderId);
		Task Refund(long orderId);
		Task Ship(long orderId);
		Task Persist(Models.Order order);
	}
}