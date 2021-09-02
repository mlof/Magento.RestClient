using System;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Orders;
using Magento.RestClient.Repositories.Abstractions.Customers;
using Magento.RestClient.Search;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IReadOrderRepository
    {
        Order CreateOrder(Order order);
        Order GetByOrderId(long orderId);
    }
}