using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Requests;
using Magento.RestClient.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class ProductAsyncRepository : AbstractRepository, IProductAsyncRepository
	{
		public ProductAsyncRepository(IContext context) : base(context)
		{
		}

		public Task<BulkActionResponse> Post(params Product[] models)
		{
			var request = new RestRequest("products", Method.Post);
			request.SetScope("all/async/bulk");

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