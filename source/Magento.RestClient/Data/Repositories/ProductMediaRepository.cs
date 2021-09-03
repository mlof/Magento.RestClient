using System;
using System.Collections.Generic;
using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ProductMediaRepository : IProductMediaRepository
	{
		private readonly IRestClient _client;

		public ProductMediaRepository(IRestClient client)
		{
			_client = client;
		}

		public void Create(string sku, ProductMedia entry)
		{
			throw new NotImplementedException();
		}

		public List<ProductMedia> GetForSku(string sku)
		{
			throw new NotImplementedException();
		}

		public void Delete(string sku, int entryId)
		{
			throw new NotImplementedException();
		}

		public ProductMedia Get(string sku, int entryId)
		{
			throw new NotImplementedException();
		}

		public ProductMedia Update(string sku, int entryId, ProductMedia entry)
		{
			throw new NotImplementedException();
		}
	}
}