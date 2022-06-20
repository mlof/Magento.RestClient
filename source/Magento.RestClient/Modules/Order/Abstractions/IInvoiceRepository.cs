using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Order.Models;

namespace Magento.RestClient.Modules.Order.Abstractions
{
	public interface IInvoiceRepository : IHasQueryable<Invoice>
	{
		Task<List<Invoice>> GetByOrderId(long orderId);
	}
}