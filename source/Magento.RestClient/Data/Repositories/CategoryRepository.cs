using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using Magento.RestClient.Data.Models.Category;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Search.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class CategoryRepository : AbstractRepository, ICategoryRepository
	{
		private readonly IRestClient _client;
		private readonly CategoryValidator categoryValidator;

		public CategoryRepository(IRestClient client)
		{
			_client = client;
			categoryValidator = new CategoryValidator();
		}

		private IQueryable<Category> _categoryRepositoryImplementation =>
			new MagentoQueryable<Category>(_client, "categories/list");

		public Category GetCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}") {Method = Method.GET};

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			var response = _client.Execute<Category>(request);
			return HandleResponse(response);
		}

		public CategoryTree GetCategoryTree(long? rootCategoryId = null, long? depth = null)
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

			var response = _client.Execute<CategoryTree>(request);
			return HandleResponse(response);
		}


		public void DeleteCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}") {Method = Method.DELETE};

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			_client.Execute(request);
		}

		public void MoveCategory(int categoryId, int parentId, int? afterId = null)
		{
			throw new NotImplementedException();
		}

		public List<ProductLink> GetProducts(long categoryId)
		{
			var request = new RestRequest("categories/{id}/products");
			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

			request.Method = Method.GET;
			var response = _client.Execute<List<ProductLink>>(request);
			return HandleResponse(response);
		}

		public void AddProduct(long categoryId, ProductLink productLink)
		{
			var request = new RestRequest("categories/{id}/products");
			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);

			request.Method = Method.PUT;
			request.AddJsonBody(new {productLink});
			var response = _client.Execute(request);
			HandleResponse(response);
		}

		public void DeleteProduct(int categoryId, string sku)
		{
			throw new NotImplementedException();
		}

		public Category CreateCategory(Category category)
		{
			categoryValidator.ValidateAndThrow(category);

			var request = new RestRequest("categories");

			request.Method = Method.POST;
			request.AddJsonBody(new {category});
			var response = _client.Execute<Category>(request);
			return HandleResponse(response);
		}

		public Category UpdateCategory(long categoryId, Category category)
		{
			var request = new RestRequest("categories/{id}");
			request.Method = Method.PUT;
			request.AddJsonBody(new {category});

			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);
			var response = _client.Execute<Category>(request);
			return HandleResponse(response);
		}

		public IEnumerator<Category> GetEnumerator()
		{
			return this._categoryRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) this._categoryRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => this._categoryRepositoryImplementation.ElementType;

		public Expression Expression => this._categoryRepositoryImplementation.Expression;

		public IQueryProvider Provider => this._categoryRepositoryImplementation.Provider;
	}
}