using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Data.Models.Shipping;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IShipmentRepository : IQueryable<Shipment>
	{
		List<Shipment> GetByOrderId(long orderId);
		long CreateShipment(long orderId);
	}
}