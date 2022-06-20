using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.AsynchronousOperations.Models;
using Magento.RestClient.Modules.Catalog.Models.Category;
using Magento.RestClient.Modules.Catalog.Models.Products;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface ICategoryRepository : IHasQueryable<CategoryTree>
	{
		public Task<Category> GetCategoryById(long categoryId, string scope = "all");
		public Task DeleteCategoryById(long categoryId);
		public Task MoveCategory(int categoryId, int parentId, int? afterId = null);
		public Task<List<ProductLink>> GetProducts(long categoryId);
		public Task AddProduct(long categoryId, ProductLink productLink);
		public Task DeleteProduct(int categoryId, string sku);
		public Task<Category> CreateCategory(Category category, string scope = "all");
		Task<Category> UpdateCategory(long categoryId, Category category, string scope = "all");

		public Task<CategoryTree> GetCategoryTree(long? rootCategoryId = null, long? depth = null);
		Task<BulkActionResponse> AddProductAsync(long categoryId, ProductLink link);
	}
}