using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Domain.Abstractions
{
	public interface IProductModel
	{
		Product GetProduct();
	}
}