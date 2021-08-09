using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(IRestClient client)
        {
        }

        public Category GetCategoryById()
        {
            throw new System.NotImplementedException();
        }

        public Category GetCategoryTree(int rootCategoryId, int depth = 1)
        {
            throw new System.NotImplementedException();
        }

        public SearchResponse<Category> Search()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCategoryById(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public void MoveCategory(int categoryId, int parentId, int? afterId = null)
        {
            throw new System.NotImplementedException();
        }

        public List<ProductLink> GetProducts(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public void AddProduct(int categoryId, ProductLink productLink)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProduct(int categoryId, string sku)
        {
            throw new System.NotImplementedException();
        }

        public Category CreateCategory(Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}