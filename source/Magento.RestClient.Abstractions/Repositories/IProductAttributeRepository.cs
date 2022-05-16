using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Context
{
	public interface IProductAttributeRepository : IHasQueryable<ProductAttribute>
	{
	}
}