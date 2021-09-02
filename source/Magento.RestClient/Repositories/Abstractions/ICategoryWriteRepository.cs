using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Category;
using Magento.RestClient.Models.Products;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface ICategoryWriteRepository
    {
        public void DeleteCategoryById(long categoryId);
        public void MoveCategory(int categoryId, int parentId, int? afterId = null);
        public List<ProductLink> GetProducts(long categoryId);
        public void AddProduct(long categoryId, ProductLink productLink);
        public void DeleteProduct(int categoryId, string sku);
        public Category CreateCategory(Category category);
        public Category UpdateCategory(long categoryId, Category category);

	}
}