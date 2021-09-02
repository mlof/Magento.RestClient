﻿using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using RestSharp;

namespace Magento.RestClient.Repositories
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