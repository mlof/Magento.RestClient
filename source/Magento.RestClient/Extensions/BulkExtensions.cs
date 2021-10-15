using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Data.Repositories.Requests;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Models.Catalog;
using Serilog;

namespace Magento.RestClient.Extensions
{
	public static class BulkExtensions
	{
		public async static Task<BulkActionResponse> CreateOrUpdate(this IAdminContext context,
			IEnumerable<IProductModel> models)
		{
			var productModels = models.ToList();
			Log.Information("Upserting {Count} products in bulk", productModels.Count());
			var sw = Stopwatch.StartNew();


			var products = productModels.Select(model => model.GetProduct()).ToArray();
			var createProductsResponse = await context.Bulk.CreateOrUpdateProducts(products);
			Log.Information("Created bulk action {Guid}", createProductsResponse.BulkUuid);
			await context.Bulk.AwaitBulkOperations(createProductsResponse);

			sw.Stop();
			Log.Information("Upserted {Count} products in {Elapsed}", productModels.Count(), sw.Elapsed);


			var mediaRequests = productModels.SelectMany(model => model.MediaEntries,
				(model, entry) => new CreateOrUpdateMediaRequest() { Sku = model.Sku, Entry = entry }).ToArray();
			var createOrUpdateMediaResponse  = await context.Bulk.CreateOrUpdateMedia(mediaRequests);
			await context.Bulk.AwaitBulkOperations(createOrUpdateMediaResponse);

			return createProductsResponse;
		}
	}
}