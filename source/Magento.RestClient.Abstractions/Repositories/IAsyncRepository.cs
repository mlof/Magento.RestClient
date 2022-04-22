using System;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Requests;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IAsyncRepository : IHasQueryable<BulkOperation>
	{
		Task<BulkOperation> GetStatus(Guid uuid);

		Task<BulkOperation> AwaitBulkOperations(Guid uuid, TimeSpan? delay = null);

	




	}

	public interface IProductAsyncRepository
	{
		Task<BulkActionResponse> Post(params Product[] models);
		Task<BulkActionResponse> PostMediaBySku(params CreateOrUpdateMediaRequest[] media);
	}
}