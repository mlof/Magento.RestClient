using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Search.Extensions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IRestClient _client;

		private IQueryable<Product> ProductRepositoryImplementation =>
			new MagentoQueryable<Product>(_client, "products");

		public ProductRepository(IRestClient client)
		{
			_client = client;
		}


		public Product GetProductBySku(string sku, string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.SetScope(scope);


			var response = _client.Execute<Product>(request);

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

		public Product CreateProduct(Product product, bool saveOptions = true)
		{
			var request = new RestRequest("products") {Method = Method.POST};
			request.SetScope("all");
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});
			var response = _client.Execute<Product>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public Product UpdateProduct(string sku, Product product, bool saveOptions = true, string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.SetScope(scope);
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.Method = Method.PUT;
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});

			var response = _client.Execute<Product>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}

		public void DeleteProduct(string sku)
		{
			var request = new RestRequest("products/{sku}") {Method = Method.DELETE};
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);


			var response = _client.Execute<Product>(request);
		}

		public IEnumerator<Product> GetEnumerator()
		{
			return this.ProductRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) this.ProductRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => this.ProductRepositoryImplementation.ElementType;

		public Expression Expression => this.ProductRepositoryImplementation.Expression;

		public IQueryProvider Provider => this.ProductRepositoryImplementation.Provider;
	}
}