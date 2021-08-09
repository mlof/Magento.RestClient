using System.Collections.Generic;
using System.Net;
using Magento.RestClient.Extensions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRestClient _client;

        public CategoryRepository(IRestClient client)
        {
            this._client = client;
        }

        public Category GetCategoryById(int categoryId)
        {
            var request = new RestRequest("categories/{categoryId}") { Method = Method.GET};

            request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

            var response = _client.Execute<Category>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw response.GetException();
            }
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