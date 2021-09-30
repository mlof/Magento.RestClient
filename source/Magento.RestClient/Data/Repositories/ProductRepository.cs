using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IRestClient _client;

		public ProductRepository(IRestClient client)
		{
			_client = client;
		}


		async public Task<Product> GetProductBySku(string sku, string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.SetScope(scope);


			var response = await _client.ExecuteAsync<Product>(request);

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

		async public Task<Product> CreateProduct(Product product, bool saveOptions = true)
		{
			var request = new RestRequest("products") {Method = Method.POST};
			request.SetScope("all");
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});
			var response = await _client.ExecuteAsync<Product>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		async public Task<Product> UpdateProduct(string sku, Product product, bool saveOptions = true,
			string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.SetScope(scope);
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.Method = Method.PUT;
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});

			var response = await _client.ExecuteAsync<Product>(request);

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


			var response = await _client.ExecuteAsync<Product>(request);
		}


		public IQueryable<Product> AsQueryable()
		{
			return new MagentoQueryable<Product>(_client, "products");
		}

		async public Task<BulkActionResponse> Save(params Product[] models)
		{
			var request = new RestRequest("products");
			request.Method = Method.POST;
			request.SetScope("all/async/bulk");

			request.AddJsonBody(
				models.Select(product => new {product = product}).ToList()
			);


			var response = await _client.ExecuteAsync<BulkActionResponse>(request);

			return response.Data;
		}
	}
}