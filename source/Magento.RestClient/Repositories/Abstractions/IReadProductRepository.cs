using Magento.RestClient.Models.Products;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface IReadProductRepository
	{
		Product GetProductBySku(string sku, string scope = "all");
	}
}