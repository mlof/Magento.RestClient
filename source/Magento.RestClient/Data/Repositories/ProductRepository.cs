using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class ProductRepository : AbstractRepository, IProductRepository
	{

		public ProductRepository(IContext context) : base(context)
		{
		}


		public async Task<Product> GetProductBySku(string sku, string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.SetScope(scope);


			var response = await Client.ExecuteAsync<Product>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}

			throw response.ErrorException;
		}

		public async Task<Product> CreateProduct(Product product, bool saveOptions = true)
		{
			var request = new RestRequest("products") {Method = Method.POST};
			request.SetScope("all");
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});
			var response = await Client.ExecuteAsync<Product>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public async Task<Product> UpdateProduct(string sku, Product product, bool saveOptions = true,
			string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.SetScope(scope);
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.Method = Method.PUT;
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});

			var response = await Client.ExecuteAsync<Product>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}

		public async Task DeleteProduct(string sku)
		{
			var request = new RestRequest("products/{sku}") {Method = Method.DELETE};
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);


			var response = await Client.ExecuteAsync<Product>(request);
		}


		public IQueryable<Product> AsQueryable()
		{
			return new MagentoQueryable<Product>(Client, "products");
		}

		public async Task<BulkActionResponse> Save(params Product[] models)
		{
			var request = new RestRequest("products");
			request.Method = Method.POST;
			request.SetScope("all/async/bulk");

			request.AddJsonBody(
				models.Select(product => new {product = product}).ToList()
			);


			var response = await Client.ExecuteAsync<BulkActionResponse>(request);

			return response.Data;
		}
	}
}