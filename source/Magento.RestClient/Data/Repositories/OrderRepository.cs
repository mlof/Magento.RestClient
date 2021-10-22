using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Expressions;
using Magento.RestClient.Validators;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class OrderRepository : AbstractRepository, IOrderRepository
	{
		private readonly OrderValidator _orderValidator;

		public OrderRepository(IContext context) : base(context)
		{
			_orderValidator = new OrderValidator();
		}

		public async Task<Order> CreateOrder(Order order)
		{
			await _orderValidator.ValidateAsync(order, options => options.ThrowOnFailures()).ConfigureAwait(false);

			var request = new RestRequest("orders", Method.POST);
			request.AddJsonBody(new {entity = order});

			return await ExecuteAsync<Order>(request).ConfigureAwait(false);
		}

		public Task<Order> GetByOrderId(long orderId)
		{
			IRestRequest request = new RestRequest("orders/{id}", Method.GET);
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

		/// <summary>
		/// CreateInvoice
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException"></exception>
		public Task CreateInvoice(long orderId)
		{
			IRestRequest request = new RestRequest("order/{id}/invoice", Method.POST);
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			request.AddJsonBody(new {capture = true, notify = true});
			return ExecuteAsync(request);
		}

		public IQueryable<Order> AsQueryable()
		{
			return new MagentoQueryable<Order>(this.Client, "orders");
		}
	}
}