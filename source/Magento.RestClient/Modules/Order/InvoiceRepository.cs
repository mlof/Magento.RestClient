using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Order.Abstractions;
using Magento.RestClient.Modules.Order.Models;

namespace Magento.RestClient.Modules.Order
{
	public class InvoiceRepository : AbstractRepository, IInvoiceRepository
	{
		public InvoiceRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public Task<List<Invoice>> GetByOrderId(long orderId)
		{
			var response = AsQueryable().Where(invoice => invoice.OrderId == orderId).ToList();
			return Task.FromResult(response);
		}

		public IQueryable<Invoice> AsQueryable()
		{
			return new MagentoQueryable<Invoice>(this.Client, "invoices");
		}
	}
}