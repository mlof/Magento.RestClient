using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Shipping;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ShipmentRepository : AbstractRepository, IShipmentRepository
	{

		public ShipmentRepository(IContext context) : base(context)
		{
		}


		public List<Shipment> GetByOrderId(long orderId)
		{
			var response = this.AsQueryable().Where(shipment => shipment.OrderId == orderId).ToList();
			return response;
		}

		public async Task<long> CreateShipment(long orderId)
		{
			var request = new RestRequest("order/{orderId}/ship");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("orderId", orderId, ParameterType.UrlSegment);

			var response = await Client.ExecuteAsync<long>(request);

			return response.Data;
		}

		public IQueryable<Shipment> AsQueryable()
		{
			return new MagentoQueryable<Shipment>(Client, "shipments");
		}
	}
}