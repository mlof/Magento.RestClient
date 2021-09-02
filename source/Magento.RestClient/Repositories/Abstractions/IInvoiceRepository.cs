using System.Collections.Generic;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IInvoiceRepository
    {
	    List<Invoice> GetByOrderId(long orderId);
    }
}