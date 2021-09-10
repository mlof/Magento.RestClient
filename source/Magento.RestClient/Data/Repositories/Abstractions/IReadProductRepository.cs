using System.Linq;
using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IReadProductRepository 
	{
		Product GetProductBySku(string sku, string scope = "all");
	}
}