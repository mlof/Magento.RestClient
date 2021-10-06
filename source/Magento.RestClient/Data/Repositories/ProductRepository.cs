using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
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

			
				return await ExecuteAsync<Product>(request).ConfigureAwait(false);
		}

		public async Task<Product> CreateProduct(Product product, bool saveOptions = true)
		{
			var request = new RestRequest("products") {Method = Method.POST};
			request.SetScope("all");
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});
			return await ExecuteAsync<Product>(request).ConfigureAwait(false);
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

			return await ExecuteAsync<Product>(request).ConfigureAwait(false);
		}

		public async Task DeleteProduct(string sku)
		{
			var request = new RestRequest("products/{sku}") {Method = Method.DELETE};
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);

			await ExecuteAsync<Product>(request).ConfigureAwait(false);
		}

		public IQueryable<Product> AsQueryable()
		{
			return new MagentoQueryable<Product>(this.Client, "products");
		}

		public Task<BulkActionResponse> Save(params Product[] models)
		{
			var request = new RestRequest("products", Method.POST);
			request.SetScope("all/async/bulk");

			request.AddJsonBody(
				models.Select(product => new {product = product}).ToList()
			);

			return ExecuteAsync<BulkActionResponse>(request);
		}
	}
}