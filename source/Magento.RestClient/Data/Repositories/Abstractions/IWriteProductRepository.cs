using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IWriteProductRepository
	{
		Product CreateProduct(Product product, bool saveOptions = true);
		Product UpdateProduct(string sku, Product product, bool saveOptions = true, string scope = "all");
		void DeleteProduct(string sku);
	}
}