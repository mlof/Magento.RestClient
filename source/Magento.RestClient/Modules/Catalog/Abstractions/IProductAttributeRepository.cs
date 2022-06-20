using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IProductAttributeRepository : IHasQueryable<ProductAttribute>
	{
	}
}