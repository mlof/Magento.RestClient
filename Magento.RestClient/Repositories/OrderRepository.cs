using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRestClient _client;

        public OrderRepository(IRestClient client)
        {
            this._client = client;
        }

        public SearchResponse<Order> Search()
        {
            throw new System.NotImplementedException();
        }

        public Order CreateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Order GetOrderById(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void Cancel(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void Hold(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void Unhold(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void Refund(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void Ship(int orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}