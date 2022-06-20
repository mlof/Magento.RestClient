using System.Threading.Tasks;
using Magento.RestClient.Modules.AsynchronousOperations.Models;
using Magento.RestClient.Modules.Catalog.Models.Products;
using Magento.RestClient.Requests;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IProductAsyncRepository
	{
		Task<BulkActionResponse> Post(params Product[] models);
		Task<BulkActionResponse> PostMediaBySku(params CreateOrUpdateMediaRequest[] media);
	}
}