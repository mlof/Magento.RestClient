using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;
using RestSharp;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IProductRepository : IHasQueryable<Product>
	{
		Task<Product> GetProductBySku(string sku, string scope = "all");

		Task<Product> CreateProduct(Product product, bool saveOptions = true);
		RestRequest GetCreateProductRequest(Product product, bool saveOptions = true);

		Task<Product> UpdateProduct(string sku, Product product, bool saveOptions = true, string scope = null);
		Task DeleteProduct(string sku);
		Task<ProductOption> CreateOption(ProductOption productOption);
	}
}