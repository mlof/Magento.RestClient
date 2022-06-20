using System.Collections.Generic;
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
	public class ProductAttributeAsyncRepository : AbstractRepository, IProductAttributeAsyncRepository
	{
		public ProductAttributeAsyncRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public Task<BulkActionResponse> PutAttributesByAttributeCode(params ProductAttribute[] attributes)
		{
			var maxOptionsPerRequest = 15;

			var request = new RestRequest("products/attributes/byAttributeCode", Method.Put);
			request.SetScope("all/async/bulk");

			var requests = new List<CreateOrUpdateAttributeRequest>();

			foreach (var attribute in attributes)
			{
				if (attribute.Options == null || attribute.Options.Count <= maxOptionsPerRequest)
				{
					requests.Add(
						new CreateOrUpdateAttributeRequest {
							AttributeCode = attribute.AttributeCode, Attribute = attribute
						});
				}
				else
				{
					foreach (var options in attribute.Options.Chunk(maxOptionsPerRequest))
					{
						requests.Add(
							new CreateOrUpdateAttributeRequest {
								AttributeCode = attribute.AttributeCode,
								Attribute = attribute with {Options = options.ToList()}
							});
					}
				}
			}

			request.AddJsonBody(requests);

			return ExecuteAsync<BulkActionResponse>(request);
		}
	}
}