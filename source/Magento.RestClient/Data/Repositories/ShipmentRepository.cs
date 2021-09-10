using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Magento.RestClient.Data.Models.Shipping;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Search;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ShipmentRepository : IShipmentRepository
	{
		private readonly IRestClient _client;

		private IQueryable<Shipment> _shipmentRepositoryImplementation =>
			new MagentoQueryable<Shipment>(_client, "shipments");

		public ShipmentRepository(IRestClient client)
		{
			_client = client;
		}


		public List<Shipment> GetByOrderId(long orderId)
		{
			var response = this.Where(shipment => shipment.OrderId == orderId).ToList();
			return response;
		}

		public long CreateShipment(long orderId)
		{
			var request = new RestRequest("order/{orderId}/ship");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("orderId", orderId, ParameterType.UrlSegment);

			var response = _client.Execute<long>(request);

			return response.Data;
		}

		public IEnumerator<Shipment> GetEnumerator()
		{
			return _shipmentRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _shipmentRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => _shipmentRepositoryImplementation.ElementType;

		public Expression Expression => _shipmentRepositoryImplementation.Expression;

		public IQueryProvider Provider => _shipmentRepositoryImplementation.Provider;
	}
}