using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Catalog.Category;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class CategoryRepository : AbstractRepository, ICategoryRepository
	{
		private readonly CategoryValidator _categoryValidator;

		public CategoryRepository(IContext context) : base(context)
		{
			_categoryValidator = new CategoryValidator();
		}

		public Task<Category> GetCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}", Method.GET);

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			return ExecuteAsync<Category>(request);
		}

		public Task<CategoryTree> GetCategoryTree(long? rootCategoryId = null, long? depth = null)
		{
			var request = new RestRequest("categories", Method.GET);

			if (rootCategoryId != null)
			{
				request.AddOrUpdateParameter("rootCategoryId", rootCategoryId);
			}

			if (depth != null)
			{
				request.AddOrUpdateParameter("depth", depth);
			}

			return ExecuteAsync<CategoryTree>(request);
		}

		public Task DeleteCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}", Method.DELETE);

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			return this.Client.ExecuteAsync(request);
		}

		public async Task MoveCategory(int categoryId, int parentId, int? afterId = null)
		{

			var request = new RestRequest("categories/{categoryId}/move", Method.PUT);

			request.AddUrlSegment("categoryId", categoryId);
			request.AddJsonBody(new { parentId, afterId });

			await this.Client.ExecuteAsync(request);
		}

		public Task<List<ProductLink>> GetProducts(long categoryId)
		{
			var request = new RestRequest("categories/{id}/products", Method.GET);
			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

			return ExecuteAsync<List<ProductLink>>(request);
		}

		public Task AddProduct(long categoryId, ProductLink productLink)
		{
			var request = new RestRequest("categories/{id}/products", Method.PUT);
			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

			request.AddJsonBody(new { productLink });
			return ExecuteAsync(request);
		}

		public Task DeleteProduct(int categoryId, string sku)
		{
			throw new NotImplementedException();
		}

		public async Task<Category> CreateCategory(Category category)
		{
			await _categoryValidator.ValidateAndThrowAsync(category).ConfigureAwait(false);

			var request = new RestRequest("categories", Method.POST);

			request.AddJsonBody(new { category });
			return await ExecuteAsync<Category>(request).ConfigureAwait(false);
		}

		public Task<Category> UpdateCategory(long categoryId, Category category)
		{
			var request = new RestRequest("categories/{id}", Method.PUT);
			request.AddJsonBody(new { category });

			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);
			return ExecuteAsync<Category>(request);
		}

		public IQueryable<Category> AsQueryable()
		{
			return new MagentoQueryable<Category>(this.Client, "categories/list");
		}
	}
}