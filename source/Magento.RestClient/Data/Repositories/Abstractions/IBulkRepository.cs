﻿using System;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories.Requests;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IBulkRepository : IHasQueryable<BulkOperation>
	{
		Task<BulkOperation> GetStatus(Guid uuid);

		Task<BulkOperation> AwaitBulkOperations(Guid uuid, TimeSpan? delay = null);
		Task<BulkActionResponse> CreateOrUpdateProducts(params Product[] models);

		public Task<BulkActionResponse> CreateOrUpdateConfigurableOptions(
			params ConfigurableProductOptionRequest[] requests);

		Task<BulkActionResponse> CreateOrUpdateAttributes(params ProductAttribute[] attributes);

		Task<BulkActionResponse> CreateOrUpdateConfigurations(params
			CreateOrUpdateConfigurationRequest[] configurations);

		Task<BulkActionResponse> AssignProductsByCategoryId(long categoryId, params string[] skus);

		Task<BulkActionResponse> CreateOrUpdateMedia(params CreateOrUpdateMediaRequest[] media);
	}
}