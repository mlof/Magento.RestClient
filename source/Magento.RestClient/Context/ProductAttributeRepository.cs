using System.Linq;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Expressions;

namespace Magento.RestClient.Context
{
	public class ProductAttributeRepository : AbstractRepository, IProductAttributeRepository
	{
		public ProductAttributeRepository(MagentoAdminContext magentoAdminContext) : base(magentoAdminContext)
		{
		}

		public IQueryable<ProductAttribute> AsQueryable()
		{
			return new MagentoQueryable<ProductAttribute>(this.Client, "products/attributes");
		}
	}
}