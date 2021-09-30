using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Shipping;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IShipmentRepository : IHasQueryable<Shipment>
	{
		List<Shipment> GetByOrderId(long orderId);
		Task<long> CreateShipment(long orderId);
	}
}