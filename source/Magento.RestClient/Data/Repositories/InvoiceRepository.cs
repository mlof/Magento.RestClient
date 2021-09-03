using System.Collections.Generic;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Repositories.Abstractions;
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


		public List<Invoice> GetByOrderId(long orderId)
		{
			var response = _client.Search()
				.Invoices(builder => builder.WhereEquals(invoice => invoice.OrderId, orderId));
			return response.Items;
		}
	}
}