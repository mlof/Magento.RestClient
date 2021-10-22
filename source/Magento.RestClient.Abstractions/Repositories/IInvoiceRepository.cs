using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Invoices;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IInvoiceRepository : IHasQueryable<Invoice>
	{
		Task<List<Invoice>> GetByOrderId(long orderId);
	}
}