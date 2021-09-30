using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Shipping;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
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
			var response = this.AsQueryable().Where(shipment => shipment.OrderId == orderId).ToList();
			return response;
		}

		async public Task<long> CreateShipment(long orderId)
		{
			var request = new RestRequest("order/{orderId}/ship");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("orderId", orderId, ParameterType.UrlSegment);

			var response = await _client.ExecuteAsync<long>(request);

			return response.Data;
		}

		public IQueryable<Shipment> AsQueryable()
		{
			return new MagentoQueryable<Shipment>(_client, "shipments");
		}
	}
}