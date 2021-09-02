using System;
using FluentValidation;
using Magento.RestClient.Models.Orders;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Validators;
using RestSharp;

namespace Magento.RestClient.Repositories
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


		public Order CreateOrder(Order order)
		{
			_orderValidator.Validate(order, options => options.ThrowOnFailures());

			var request = new RestRequest("orders");
			request.Method = Method.POST;
			request.AddJsonBody(new {entity = order});

			var response = _client.Execute(request);
			return order;
		}

		public Order GetByOrderId(long orderId)
		{
			IRestRequest request = new RestRequest("orders/{id}");
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			var response = _client.Execute<Order>(request);
			return response.Data;
		}

		public void Cancel(long orderId)
		{
			throw new NotImplementedException();
		}

		public void Hold(long orderId)
		{
			throw new NotImplementedException();
		}

		public void Unhold(long orderId)
		{
			throw new NotImplementedException();
		}

		public void Refund(long orderId)
		{
			throw new NotImplementedException();
		}

		public void Ship(long orderId)
		{
			throw new NotImplementedException();
		}

		public void CreateInvoice(long orderId)
		{
			IRestRequest request = new RestRequest("order/{id}/invoice");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("id", orderId, ParameterType.UrlSegment);
			request.AddJsonBody(new {capture = true, notify = true});
			var response = _client.Execute(request);
		}
	}
}