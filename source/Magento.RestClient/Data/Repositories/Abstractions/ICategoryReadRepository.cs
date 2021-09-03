using Magento.RestClient.Data.Models.Category;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ICategoryReadRepository
	{
		public Category GetCategoryById(long categoryId);

		public CategoryTree GetCategoryTree(long? rootCategoryId = null, long? depth = null);
	}
}