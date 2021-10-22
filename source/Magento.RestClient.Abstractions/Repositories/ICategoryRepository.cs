using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Category;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface ICategoryRepository : IHasQueryable<Category>
	{
		public Task<Category> GetCategoryById(long categoryId);
		public Task DeleteCategoryById(long categoryId);
		public Task MoveCategory(int categoryId, int parentId, int? afterId = null);
		public Task<List<ProductLink>> GetProducts(long categoryId);
		public Task AddProduct(long categoryId, ProductLink productLink);
		public Task DeleteProduct(int categoryId, string sku);
		public Task<Category> CreateCategory(Category category);
		public Task<Category> UpdateCategory(long categoryId, Category category);
		public Task<CategoryTree> GetCategoryTree(long? rootCategoryId = null, long? depth = null);
	}
}