using System.Collections.Generic;
using Magento.RestClient.Models.Shipping;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface IShipmentRepository 
	{
		List<Shipment> GetByOrderId(long orderId);
		long CreateShipment(long orderId);
	}
}