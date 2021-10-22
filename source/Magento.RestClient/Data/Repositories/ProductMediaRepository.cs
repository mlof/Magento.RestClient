using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ProductMediaRepository : AbstractRepository, IProductMediaRepository
	{
		public ProductMediaRepository(IContext context) : base(context)
		{
		}

		async public Task<MediaEntry> Create(string sku, MediaEntry entry)
		{
			var request = new RestRequest("products/{sku}/media", Method.POST);
			request.SetScope("all");

			request.AddUrlSegment("sku", sku);

			request.AddJsonBody(new {entry});
			var response =  await ExecuteAsync<long>(request).ConfigureAwait(false);

			entry.Id = response;
			return entry;
		}

		public Task<List<MediaEntry>> GetForSku(string sku)
		{
			var request = new RestRequest("products/{sku}/media", Method.GET);
			request.AddUrlSegment("sku", sku);
			request.SetScope("all");

			return ExecuteAsync<List<MediaEntry>>(request);
		}

		public Task<bool> Delete(string sku, long entryId)
		{
			var request = new RestRequest("products/{sku}/media/{entryId}", Method.DELETE);
			request.SetScope("all");
			request.AddUrlSegment("sku", sku);
			request.AddUrlSegment("entryId", entryId);

			return ExecuteAsync<bool>(request);
		}

		public Task<MediaEntry> Get(string sku, int entryId)
		{
			var request = new RestRequest("products/{sku}/media/{entryId}", Method.GET);
			request.SetScope("all");
			request.AddUrlSegment("sku", sku);
			request.AddUrlSegment("entryId", entryId);

			return ExecuteAsync<MediaEntry>(request);
		}

		public async Task<MediaEntry> Update(string sku, long entryId, MediaEntry entry)
		{
			var request = new RestRequest("products/{sku}/media/{entryId}", Method.PUT);
			request.SetScope("all");

			request.AddUrlSegment("sku", sku);
			request.AddUrlSegment("entryId", entryId);
			entry.Id = entryId;

			request.AddJsonBody(new {entry});
			var response = await ExecuteAsync<long>(request).ConfigureAwait(false);

			entry.Id = response;
			return entry;
		}
	}
}