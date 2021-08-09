using System.Collections.Generic;
using MagentoApi.Exceptions;
using MagentoApi.Extensions;
using MagentoApi.Models;
using MagentoApi.Repositories.Abstractions;
using RestSharp;

namespace MagentoApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRestClient _client;

        public ProductRepository(IRestClient client)
        {
            this._client = client;
        }

        public List<SearchResponse<Product>> Search()
        {
            throw new System.NotImplementedException();
        }

        public Product GetProductBySku(string sku)
        {
            var request = new RestRequest("products/{sku}");
            request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);

            var response = _client.Execute<Product>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw response.GetException();
            }
        }

        public Product CreateProduct(Product product, bool saveOptions = true)
        {
            var request = new RestRequest("products");
            request.Method = Method.POST;
            request.AddJsonBody(new {product = product});
            var response = _client.Execute<Product>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw response.GetException();
            }
        }

        public Product UpdateProduct(string sku, Product product, bool saveOptions = true)
        {
            var request = new RestRequest("products/{sku}");
            request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
            request.Method = Method.PUT;
            request.AddJsonBody(new {product = product});

            var response = _client.Execute<Product>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw response.GetException();
            }
        }

        public void DeleteProduct(string sku)
        {
            var request = new RestRequest("products/{sku}");
            request.Method = Method.DELETE;
            request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);


            var response = _client.Execute<Product>(request);
        }
    }
}