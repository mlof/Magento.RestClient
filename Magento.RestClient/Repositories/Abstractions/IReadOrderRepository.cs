using System;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions.Customers;
using Magento.RestClient.Search;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IReadOrderRepository
    {
        Order CreateOrder(Order order);
        Order GetOrderById(long orderId);
    }
}