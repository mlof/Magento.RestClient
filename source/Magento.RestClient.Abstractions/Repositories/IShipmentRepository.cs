using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Shipping;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IShipmentRepository : IHasQueryable<Shipment>
	{
		List<Shipment> GetByOrderId(long orderId);
		Task<long> CreateShipment(long orderId);
	}
}