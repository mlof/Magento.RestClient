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

		public Category GetCategoryById(int categoryId)
		{
			var request = new RestRequest("categories/{categoryId}") {Method = Method.GET};

			request.AddOrUpdateParameter("categoryId", categoryId, ParameterType.UrlSegment);

			var response = _client.Execute<Category>(request);
			return HandleResponse(response);

		}

		public Category GetCategoryTree(int rootCategoryId, int depth = 1)
		{
			throw new System.NotImplementedException();
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
			categoryValidator.ValidateAndThrow(category);

			var request = new RestRequest("categories");
			
			request.Method = Method.POST;
			request.AddJsonBody(new {category});
			var response =  _client.Execute<Category>(request);
			return HandleResponse(response);

		}
	}

	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(category => category.Name).NotEmpty();
			RuleFor(category => category.IsActive).NotNull();
		}
	}
}