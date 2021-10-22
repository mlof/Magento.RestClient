using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Shipping;
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
			var response = AsQueryable().Where(shipment => shipment.OrderId == orderId).ToList();
			return response;
		}

		public Task<long> CreateShipment(long orderId)
		{
			var request = new RestRequest("order/{orderId}/ship", Method.POST);
			request.AddOrUpdateParameter("orderId", orderId, ParameterType.UrlSegment);

			return ExecuteAsync<long>(request);
		}

		public IQueryable<Shipment> AsQueryable()
		{
			return new MagentoQueryable<Shipment>(this.Client, "shipments");
		}
	}
}