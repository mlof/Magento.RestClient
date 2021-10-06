using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Invoices;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IInvoiceRepository : IHasQueryable<Invoice>
	{
		Task<List<Invoice>> GetByOrderId(long orderId);
	}
}