using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Search;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class InvoiceRepository : IInvoiceRepository
	{
		private readonly IRestClient _client;


		public InvoiceRepository(IRestClient client)
		{
			_client = client;
		}


		public Task<List<Invoice>> GetByOrderId(long orderId)
		{
			var response = this.AsQueryable().Where(invoice => invoice.OrderId == orderId).ToList();
			return Task.FromResult(response);
		}

		public IQueryable<Invoice> AsQueryable()
		{
			return new MagentoQueryable<Invoice>(_client, "invoices");
		}
	}
}