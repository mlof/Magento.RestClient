using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Data.Models;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IInvoiceRepository : IQueryable<Invoice>
	{
		List<Invoice> GetByOrderId(long orderId);
	}
}