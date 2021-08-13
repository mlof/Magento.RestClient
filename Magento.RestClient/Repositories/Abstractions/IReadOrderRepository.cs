using System;
using Magento.RestClient.Models;
using Magento.RestClient.Search;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IReadOrderRepository
    {
        SearchResponse<Order> Search(Action<SearchBuilder<Order>> builder = null);
        Order CreateOrder(Order order);
        Order GetOrderById(long orderId);
    }
}