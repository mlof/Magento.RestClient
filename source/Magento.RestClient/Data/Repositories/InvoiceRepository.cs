using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		private IQueryable<Invoice> _invoiceRepositoryImplementation =>
			new MagentoQueryable<Invoice>(_client, "invoices");

		public InvoiceRepository(IRestClient client)
		{
			_client = client;
		}


		public List<Invoice> GetByOrderId(long orderId)
		{
			var response = this.Where(invoice => invoice.OrderId ==orderId).ToList();
			return response;
		}

		public IEnumerator<Invoice> GetEnumerator()
		{
			return _invoiceRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _invoiceRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => _invoiceRepositoryImplementation.ElementType;

		public Expression Expression => _invoiceRepositoryImplementation.Expression;

		public IQueryProvider Provider => _invoiceRepositoryImplementation.Provider;
	}
}