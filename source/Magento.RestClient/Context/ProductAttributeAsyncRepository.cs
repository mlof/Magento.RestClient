using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Requests;
using Magento.RestClient.Extensions;
using RestSharp;

namespace Magento.RestClient.Context
{
	public class ProductAttributeAsyncRepository : AbstractRepository, IProductAttributeAsyncRepository
	{
		public ProductAttributeAsyncRepository(MagentoAdminContext context) : base(context)
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