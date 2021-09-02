using Magento.RestClient.Models.Category;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface ICategoryReadRepository
	{
		public Category GetCategoryById(long categoryId);

		public CategoryTree GetCategoryTree(long? rootCategoryId = null, long? depth = null);
	}
}