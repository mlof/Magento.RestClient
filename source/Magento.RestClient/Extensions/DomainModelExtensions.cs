using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Models.Catalog;

namespace Magento.RestClient.Extensions
{
	public static class DomainModelExtensions
	{
		public static Task SaveAllAsync(this IEnumerable<IDomainModel> domainModels)
		{
			return Task.WhenAll(domainModels.Select(model => model.SaveAsync()).ToList());
		}
	}

	public static class BulkExtensions
	{
		public async static Task<BulkActionResponse> CreateOrUpdate(this IAdminContext context,
			IEnumerable<IProductModel> models)
		{
			var list = new List<Product>();

			var productModels = models.ToList();
			list.AddRange(productModels.Select(model => model.GetProduct()).ToArray());

			list.AddRange(productModels.OfType<ConfigurableProductModel>().SelectMany(model => model.Children,
				(model, productModel) => productModel.GetProduct()));


			var r = await context.Products.Save(
					list.ToArray()
				);
			return r;
		}
	}
}