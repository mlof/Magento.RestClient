using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using RestSharp;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IProductRepository : IHasQueryable<Product>
	{
		Task<Product> GetProductBySku(string sku, string scope = "all");

		Task<Product> CreateProduct(Product product, bool saveOptions = true);

		IRestRequest GetCreateProductRequest(Product product, bool saveOptions = true);

		Task<Product> UpdateProduct(string sku, Product product, bool saveOptions = true, string? scope = null);
		Task DeleteProduct(string sku);

	}
}