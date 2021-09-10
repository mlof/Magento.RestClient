using System.Linq;
using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IProductRepository : IReadProductRepository, IWriteProductRepository, IQueryable<Product>
	{
	}
}