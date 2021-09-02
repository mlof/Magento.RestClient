using System.Collections.Generic;
using Magento.RestClient.Models.Shipping;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using RestSharp;

namespace Magento.RestClient
{
	internal class ShipmentRepository : IShipmentRepository
	{
		private readonly IRestClient _client;

		public ShipmentRepository(IRestClient client)
		{
			_client = client;
		}


		public List<Shipment> GetByOrderId(long orderId)
		{
			var response = _client.Search()
				.Shipments(builder => builder.WhereEquals(shipment => shipment.OrderId, orderId));
			return response.Items;
		}

		public long CreateShipment(long orderId)
		{
			var request = new RestRequest("order/{orderId}/ship");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("orderId", orderId, ParameterType.UrlSegment);

			var response = _client.Execute<long>(request);

			return response.Data;
		}
	}
}