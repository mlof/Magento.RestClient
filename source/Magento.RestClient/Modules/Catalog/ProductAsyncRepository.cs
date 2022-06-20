using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Modules.AsynchronousOperations.Models;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;
using Magento.RestClient.Requests;
using RestSharp;

namespace Magento.RestClient.Modules.Catalog
{
	public class ProductAsyncRepository : AbstractRepository, IProductAsyncRepository
	{
		public ProductAsyncRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public Task<BulkActionResponse> Post(params Product[] models)
		{
			var request = new RestRequest(MagentoContext.Options.Host + "/rest/async/bulk/V1/products", Method.Post);
			request.AddJsonBody(
				models.Select(product => new {product}).ToList()
			);

			return ExecuteAsync<BulkActionResponse>(request);
		}

		public Task<BulkActionResponse> PostMediaBySku(
			params CreateOrUpdateMediaRequest[] media)
		{
			var request = new RestRequest("products/bySku/media", Method.Post);
			request.SetScope("all/async/bulk");


			request.AddJsonBody(media.ToList());

			return ExecuteAsync<BulkActionResponse>(request);
		}
	}
}