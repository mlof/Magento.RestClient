using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Sales.Models;

namespace Magento.RestClient.Modules.Quote.Abstractions
{
	public interface IShipmentRepository : IHasQueryable<Shipment>
	{
		List<Shipment> GetByOrderId(long orderId);
		Task<long> CreateShipment(long orderId);
	}
}