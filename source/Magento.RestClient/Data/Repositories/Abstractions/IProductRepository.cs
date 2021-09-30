using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IProductRepository : IHasQueryable<Product>
	{
		Task<Product> GetProductBySku(string sku, string scope = "all");

		Task<Product> CreateProduct(Product product, bool saveOptions = true);
		Task<Product> UpdateProduct(string sku, Product product, bool saveOptions = true, string scope = "all");
		Task DeleteProduct(string sku);
		Task<BulkActionResponse> Save(params Product[] models);
	}
}