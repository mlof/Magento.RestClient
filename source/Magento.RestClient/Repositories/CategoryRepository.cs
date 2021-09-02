using System.Collections.Generic;
using System.Net;
using FluentValidation;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Category;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Models.Search;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search.Extensions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
	internal class CategoryRepository : AbstractRepository, ICategoryRepository
	{
		private readonly IRestClient _client;
		private readonly CategoryValidator categoryValidator;

		public CategoryRepository(IRestClient client)
		{
			this._client = client;
			this.categoryValidator = new CategoryValidator();
		}

		public Category GetCategoryById(long categoryId)
		{
			var request = new RestRequest("categories/{categoryId}") {Method = Method.GET};

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			var response = _client.Execute<Category>(request);
			return HandleResponse(response);

		}

		public CategoryTree GetCategoryTree(long? rootCategoryId = null, long? depth = null)
		{
			var request = new RestRequest("categories") { Method = Method.GET};
			request.SetScope("all");

			if (rootCategoryId != null)
			{
				request.AddOrUpdateParameter("rootCategoryId", rootCategoryId);
			}
			if (depth!= null)
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

			var response = _client.Execute(request);
		}

		public void MoveCategory(int categoryId, int parentId, int? afterId = null)
		{
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}

		public Category CreateCategory(Category category)
		{
			categoryValidator.ValidateAndThrow(category);

			var request = new RestRequest("categories");
			
			request.Method = Method.POST;
			request.AddJsonBody(new {category});
			var response =  _client.Execute<Category>(request);
			return HandleResponse(response);

		}

		public Category UpdateCategory(long categoryId, Category category)
		{
			var request = new RestRequest("categories/{id}");
			request.Method = Method.PUT;
			request.AddJsonBody(new { category });

			request.AddOrUpdateParameter("id", categoryId, ParameterType.UrlSegment);
			var response = _client.Execute<Category>(request);
			return HandleResponse(response);
		}
	}
}