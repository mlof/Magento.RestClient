using System.Collections.Generic;
using Magento.RestClient.Data.Models.Shipping;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IShipmentRepository
	{
		List<Shipment> GetByOrderId(long orderId);
		long CreateShipment(long orderId);
	}
}