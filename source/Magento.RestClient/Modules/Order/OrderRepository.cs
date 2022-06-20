using System;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Order.Abstractions;
using RestSharp;

namespace Magento.RestClient.Modules.Order
{
	public class OrderRepository : AbstractRepository, IOrderRepository
	{
		public OrderRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public async  Task<Models.Order> CreateOrder(Models.Order order)
		{
			var request = new RestRequest("orders", Method.Post);
			request.AddJsonBody(new {entity = order});

			return await ExecuteAsync<Models.Order>(request).ConfigureAwait(false);
		}

		public Task<Models.Order> GetByOrderId(long orderId)
		{
			var request = new RestRequest("orders/{id}");
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			return ExecuteAsync<Models.Order>(request);
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

		public Task Persist(Models.Order order)
		{
			var request = new RestRequest("orders", Method.Post);
			request.AddJsonBody(new {entity = order});
			return ExecuteAsync(request);
		}

		/// <summary>
		///     CreateInvoice
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task CreateInvoice(long orderId)
		{
			var request = new RestRequest("order/{id}/invoice", Method.Post);
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			request.AddJsonBody(new {capture = true, notify = true});
			return ExecuteAsync(request);
		}

		public IQueryable<Models.Order> AsQueryable()
		{
			return new MagentoQueryable<Models.Order>(this.Client, "orders");
		}
	}
}