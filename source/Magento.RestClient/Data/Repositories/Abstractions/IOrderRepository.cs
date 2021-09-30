using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Orders;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IOrderRepository : IHasQueryable<Order>
	{
		Task CreateInvoice(long orderId);
		Task<Order> CreateOrder(Order order);
		Task<Order> GetByOrderId(long orderId);
		Task Cancel(long orderId);
		Task Hold(long orderId);
		Task Unhold(long orderId);
		Task Refund(long orderId);
		Task Ship(long orderId);
	}
}