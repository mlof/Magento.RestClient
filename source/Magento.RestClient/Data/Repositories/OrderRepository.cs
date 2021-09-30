using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Validators;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IRestClient _client;
		private readonly OrderValidator _orderValidator;

		public OrderRepository(IRestClient client)
		{
			_client = client;
			_orderValidator = new OrderValidator();
		}


		async public Task<Order> CreateOrder(Order order)
		{
			await _orderValidator.ValidateAsync(order, options => options.ThrowOnFailures());

			var request = new RestRequest("orders");
			request.Method = Method.POST;
			request.AddJsonBody(new {entity = order});

			var response = await _client.ExecuteAsync(request);
			return order;
		}

		async public Task<Order> GetByOrderId(long orderId)
		{
			IRestRequest request = new RestRequest("orders/{id}");
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			var response = await _client.ExecuteAsync<Order>(request);
			return response.Data;
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

		async public Task CreateInvoice(long orderId)
		{
			IRestRequest request = new RestRequest("order/{id}/invoice");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			request.AddJsonBody(new {capture = true, notify = true});
			var response = await _client.ExecuteAsync(request);
		}


		public IQueryable<Order> AsQueryable()
		{
			return new MagentoQueryable<Order>(_client, "orders");
		}
	}
}