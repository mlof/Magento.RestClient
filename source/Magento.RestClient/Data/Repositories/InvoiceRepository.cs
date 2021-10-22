using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Invoices;
using Magento.RestClient.Expressions;
using Magento.RestClient.Search;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class InvoiceRepository : AbstractRepository, IInvoiceRepository
	{
		public InvoiceRepository(IContext context) : base(context)
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