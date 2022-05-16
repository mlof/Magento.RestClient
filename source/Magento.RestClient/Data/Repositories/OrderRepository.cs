using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Expressions;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Magento.RestClient.Data.Repositories
{
    public class OrderRepository : AbstractRepository, IOrderRepository
    {

        public OrderRepository(IContext context) : base(context)
        {
        }

        public async Task<Order> CreateOrder(Order order)
        {

            var request = new RestRequest("orders", Method.Post);
            request.AddJsonBody(new { entity = order });

            return await ExecuteAsync<Order>(request).ConfigureAwait(false);
        }

        public Task<Order> GetByOrderId(long orderId)
        {
            RestRequest request = new RestRequest("orders/{id}", Method.Get);
            request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
            return ExecuteAsync<Order>(request);
        }

        public Task Cancel(long orderId)
        {
            throw new NotImplementedException();
        }

        public Task Hold(long orderId)
        {
            throw new NotImplementedException();
        }

        public Task Unhold(long orderId)
        {
            throw new NotImplementedException();
        }

        public Task Refund(long orderId)
        {
            throw new NotImplementedException();
        }

        public Task Ship(long orderId)
        {
            throw new NotImplementedException();
        }

        public Task Persist(Order order)
        {

            RestRequest request = new RestRequest("orders", Method.Post);
            request.AddJsonBody(new { entity = order });
            return ExecuteAsync(request);


        }

        /// <summary>
        /// CreateInvoice
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
        public Task CreateInvoice(long orderId)
        {
            RestRequest request = new RestRequest("order/{id}/invoice", Method.Post);
            request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
            request.AddJsonBody(new { capture = true, notify = true });
            return ExecuteAsync(request);
        }

        public IQueryable<Order> AsQueryable()
        {
            return new MagentoQueryable<Order>(this.Client, "orders");
        }
    }
}