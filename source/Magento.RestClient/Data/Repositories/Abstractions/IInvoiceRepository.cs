using System.Collections.Generic;
using Magento.RestClient.Data.Models;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IInvoiceRepository
	{
		List<Invoice> GetByOrderId(long orderId);
	}
}