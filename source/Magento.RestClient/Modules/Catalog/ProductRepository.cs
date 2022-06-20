#nullable enable
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;
using RestSharp;

namespace Magento.RestClient.Modules.Catalog
{
	public class ProductRepository : AbstractRepository, IProductRepository
	{
		public ProductRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public async  Task<Product> GetProductBySku(string sku, string scope = "all")
		{
			var request = new RestRequest("products/{sku}");
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.SetScope(scope);


			return await ExecuteAsync<Product>(request).ConfigureAwait(false);
		}

		public async  Task<Product> CreateProduct(Product product, bool saveOptions = true)
		{
			var request = GetCreateProductRequest(product, saveOptions);
			return await ExecuteAsync<Product>(request).ConfigureAwait(false);
		}


		public async  Task<Product> UpdateProduct(string sku, Product product, bool saveOptions = true,
			string? scope = null)
		{
			var request = new RestRequest("products/{sku}");
			if (scope != null)
			{
				request.SetScope(scope);
			}

			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
			request.Method = Method.Put;
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});

			return await ExecuteAsync<Product>(request).ConfigureAwait(false);
		}

		public Task DeleteProduct(string sku)
		{
			var request = new RestRequest("products/{sku}") {Method = Method.Delete};
			request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);

			return ExecuteAsync(request);
		}

		public Task<ProductOption> CreateOption(ProductOption productOption)
		{
			var request = new RestRequest("products/options") { Method = Method.Post};

			request.AddJsonBody(new {option = productOption});
			return ExecuteAsync<ProductOption>(request);

		}


		public RestRequest GetCreateProductRequest(Product product, bool saveOptions = true)
		{
			var request = new RestRequest("products") {Method = Method.Post};
			request.SetScope("default");
			// ReSharper disable once RedundantAnonymousTypePropertyName
			request.AddJsonBody(new {product = product});
			return request;
		}

		public IQueryable<Product> AsQueryable()
		{
			return new MagentoQueryable<Product>(this.Client, "products");
		}
	}
}