using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Category;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class CategoryRepository : AbstractRepository, ICategoryRepository
	{
		private readonly CategoryValidator categoryValidator;

		public CategoryRepository(IContext context) : base(context)
		{
			categoryValidator = new CategoryValidator();
		}


		public async Task<Category> GetCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}") {Method = Method.GET};

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			var response = await Client.ExecuteAsync<Category>(request);
			return HandleResponse(response);
		}

		public async Task<CategoryTree> GetCategoryTree(long? rootCategoryId = null, long? depth = null)
		{
			var request = new RestRequest("categories") {Method = Method.GET};
			request.SetScope("all");

			if (rootCategoryId != null)
			{
				request.AddOrUpdateParameter("rootCategoryId", rootCategoryId);
			}

			if (depth != null)
			{
				request.AddOrUpdateParameter("depth", depth);
			}

			var response = await Client.ExecuteAsync<CategoryTree>(request);
			return HandleResponse(response);
		}


		public async Task DeleteCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}") {Method = Method.DELETE};

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			await Client.ExecuteAsync(request);
		}

		public async Task MoveCategory(int categoryId, int parentId, int? afterId = null)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ProductLink>> GetProducts(long categoryId)
		{
			var request = new RestRequest("categories/{id}/products");
			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

			request.Method = Method.GET;
			var response = await Client.ExecuteAsync<List<ProductLink>>(request);
			return HandleResponse(response);
		}

		public async Task AddProduct(long categoryId, ProductLink productLink)
		{
			var request = new RestRequest("categories/{id}/products");
			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

			request.Method = Method.PUT;
			request.AddJsonBody(new {productLink});
			var response = await Client.ExecuteAsync(request);
			HandleResponse(response);
		}

		public Task DeleteProduct(int categoryId, string sku)
		{
			throw new NotImplementedException();
		}

		public async Task<Category> CreateCategory(Category category)
		{
			await categoryValidator.ValidateAndThrowAsync(category);

			var request = new RestRequest("categories");

			request.Method = Method.POST;
			request.AddJsonBody(new {category});
			var response = await Client.ExecuteAsync<Category>(request);
			return HandleResponse(response);
		}

		public async Task<Category> UpdateCategory(long categoryId, Category category)
		{
			var request = new RestRequest("categories/{id}");
			request.Method = Method.PUT;
			request.AddJsonBody(new {category});

			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);
			var response = await Client.ExecuteAsync<Category>(request);
			return HandleResponse(response);
		}

		public IQueryable<Category> AsQueryable()
		{
			return new MagentoQueryable<Category>(Client, "categories/list");
		}
	}
}