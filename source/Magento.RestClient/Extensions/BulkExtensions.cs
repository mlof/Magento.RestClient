using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Models.Catalog;

namespace Magento.RestClient.Extensions
{
	public static class BulkExtensions
	{
		public async static Task<BulkActionResponse> CreateOrUpdate(this IAdminContext context,
			IEnumerable<IProductModel> models)
		{
			var list = new List<Product>();

			var productModels = models.ToList();
			var configurableProducts = productModels.OfType<ConfigurableProductModel>().ToList();
			list.AddRange(productModels.Select(model => model.GetProduct()).ToArray());

			list.AddRange(configurableProducts.SelectMany(model => model.Children,
				(model, productModel) => productModel.GetProduct()));


			var createProductsResponse = await context.Bulk.CreateOrUpdateProducts(
				list.ToArray()
			);


			return createProductsResponse;
		}
	}
}