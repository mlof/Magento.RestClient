using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ProductMediaRepository : AbstractRepository, IProductMediaRepository
	{
		private readonly IRestClient _client;

		public ProductMediaRepository(IContext context) : base(context)
		{
		}

		public Task Create(string sku, ProductMedia entry)
		{
			throw new NotImplementedException();
		}

		public Task<List<ProductMedia>> GetForSku(string sku)
		{
			throw new NotImplementedException();
		}

		public Task Delete(string sku, int entryId)
		{
			throw new NotImplementedException();
		}

		public Task<ProductMedia> Get(string sku, int entryId)
		{
			throw new NotImplementedException();
		}

		public Task<ProductMedia> Update(string sku, int entryId, ProductMedia entry)
		{
			throw new NotImplementedException();
		}
	}
}