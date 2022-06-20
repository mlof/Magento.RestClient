using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Quote.Abstractions;
using Magento.RestClient.Modules.Sales.Models;
using RestSharp;

namespace Magento.RestClient.Modules.Quote
{
	internal class ShipmentRepository : AbstractRepository, IShipmentRepository
	{
		public ShipmentRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public List<Shipment> GetByOrderId(long orderId)
		{
			var response = AsQueryable().Where(shipment => shipment.OrderId == orderId).ToList();
			return response;
		}

		public Task<long> CreateShipment(long orderId)
		{
			var request = new RestRequest("order/{orderId}/ship", Method.Post);
			request.AddOrUpdateParameter("orderId", orderId, ParameterType.UrlSegment);

			return ExecuteAsync<long>(request);
		}

		public IQueryable<Shipment> AsQueryable()
		{
			return new MagentoQueryable<Shipment>(this.Client, "shipments");
		}
	}
}