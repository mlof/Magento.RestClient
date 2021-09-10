using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
		private IQueryable<Order> _orderRepositoryImplementation => new MagentoQueryable<Order>(_client, "orders");

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

		public IEnumerator<Order> GetEnumerator()
		{
			return _orderRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _orderRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => _orderRepositoryImplementation.ElementType;

		public Expression Expression => _orderRepositoryImplementation.Expression;

		public IQueryProvider Provider => _orderRepositoryImplementation.Provider;
	}
}