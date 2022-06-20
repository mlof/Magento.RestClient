using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;

namespace Magento.RestClient.Modules.Catalog
{
	public class ProductAttributeRepository : AbstractRepository, IProductAttributeRepository
	{
		public ProductAttributeRepository(IMagentoContext magentoAdminMagentoContext) : base(magentoAdminMagentoContext)
		{
		}

		public IQueryable<ProductAttribute> AsQueryable()
		{
			return new MagentoQueryable<ProductAttribute>(this.Client, "products/attributes");
		}
	}
}