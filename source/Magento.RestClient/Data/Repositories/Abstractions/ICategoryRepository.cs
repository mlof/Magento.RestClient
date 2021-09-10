using System.Linq;
using Magento.RestClient.Data.Models.Category;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ICategoryRepository : ICategoryReadRepository, ICategoryWriteRepository, IQueryable<Category>
	{
	}
}