using System.Linq;
using Magento.RestClient.Data.Models.Orders;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IOrderRepository : IReadOrderRepository, IWriteOrderRepository, IQueryable<Order>
	{
		void CreateInvoice(long orderId);
	}
}