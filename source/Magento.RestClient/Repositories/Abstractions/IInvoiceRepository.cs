using System.Collections.Generic;
using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface IInvoiceRepository
	{
		List<Invoice> GetByOrderId(long orderId);
	}
}