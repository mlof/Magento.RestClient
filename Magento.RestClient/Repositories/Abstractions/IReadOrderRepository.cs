using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IReadOrderRepository
    {
        SearchResponse<Order> Search();
        Order CreateOrder(Order order);
        Order GetOrderById(int orderId);
    }
}