using FluentValidation;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Category;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magento.RestClient.Data.Repositories
{
    internal class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        private readonly CategoryValidator _categoryValidator;

        public CategoryRepository(IContext context) : base(context)
        {
            this.RelativeExpiration = TimeSpan.FromMinutes(1);
            _categoryValidator = new CategoryValidator();
        }

        public TimeSpan RelativeExpiration { get; set; }

        public Task<Category> GetCategoryById(long categoryId, string scope = "all")
        {
            var request = new RestRequest("categories/{categoryId}", Method.Get);

            request.SetScope(scope);
            request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);
            var key = Client.BuildUri(request);


            return Cache.GetOrCreateAsync<Category>(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = RelativeExpiration;


                return ExecuteAsync<Category>(request);
            });
        }

        public Task<CategoryTree> GetCategoryTree(long? rootCategoryId = null, long? depth = null)
        {
            var request = new RestRequest("categories", Method.Get);

            if (rootCategoryId != null)
            {
                request.AddOrUpdateParameter("rootCategoryId", rootCategoryId.Value);
            }

            if (depth != null)
            {
                request.AddOrUpdateParameter("depth", depth.Value);
            }

            return ExecuteAsync<CategoryTree>(request);
        }

        public Task<BulkActionResponse> AddProductAsync(long categoryId, ProductLink link)
        {
            var request = new RestRequest("categories/{categoryId}/products", Method.Post);
            request.SetScope("all/async");
            request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

            request.AddJsonBody(
                new
                {
                    productLink = link
                }
            );

            return ExecuteAsync<BulkActionResponse>(request);
        }

        public Task DeleteCategoryById(long categoryId)
        {
            var request = new RestRequest("categories/{categoryId}", Method.Delete);

            request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

            var key = Client.BuildUri(request);

            Cache.Remove(key);
            return this.Client.ExecuteAsync(request);
        }

        public async Task MoveCategory(int categoryId, int parentId, int? afterId = null)
        {
            var request = new RestRequest("categories/{categoryId}/move", Method.Put);

            request.AddUrlSegment("categoryId", categoryId);
            request.AddJsonBody(new { parentId, afterId });

            await this.Client.ExecuteAsync(request);
        }

        public Task<List<ProductLink>> GetProducts(long categoryId)
        {
            var request = new RestRequest("categories/{id}/products", Method.Get);
            request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

            return ExecuteAsync<List<ProductLink>>(request);
        }

        public Task AddProduct(long categoryId, ProductLink productLink)
        {
            var request = new RestRequest("categories/{id}/products", Method.Put);
            request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

            request.AddJsonBody(new { productLink });
            var key = Client.BuildUri(request);

            Cache.Remove(key);
            return ExecuteAsync(request);
        }

        public Task DeleteProduct(int categoryId, string sku)
        {
            var request = new RestRequest("categories/{id}/products/{sku}", Method.Delete);
            request.AddUrlSegment("id", categoryId);
            request.AddUrlSegment("sku", sku);
            return ExecuteAsync(request);
        }


        public async Task<Category> CreateCategory(Category category, string scope = "all")
        {
            await _categoryValidator.ValidateAndThrowAsync(category).ConfigureAwait(false);

            var request = new RestRequest("categories", Method.Post);

            request.SetScope(scope);
            request.AddJsonBody(new { category });
            return await ExecuteAsync<Category>(request).ConfigureAwait(false);
        }

        public Task<Category> UpdateCategory(long categoryId, Category category, string scope = "all")
        {
            var request = new RestRequest("categories/{id}", Method.Put);
            request.AddJsonBody(new { category });

            request.SetScope(scope);
            request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);
            var key = Client.BuildUri(request);

            Cache.Remove(key);
            return ExecuteAsync<Category>(request);
        }

        public IQueryable<CategoryTree> AsQueryable()
        {
            return new MagentoQueryable<CategoryTree>(this.Client, "categories/list");
        }
    }
}