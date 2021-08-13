using System.Collections.Generic;
using System.Net;
using FluentValidation;
using Magento.RestClient.Extensions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Validators;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRestClient _client;
        private readonly ProductValidator productValidator;

        public ProductRepository(IRestClient client)
        {
            this._client = client;
            this.productValidator = new ProductValidator();
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
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else {
                throw response.ErrorException;
            }
        }

        public Product CreateProduct(Product product, bool saveOptions = true)
        {
            productValidator.ValidateAndThrow(product);


            var request = new RestRequest("products") {Method = Method.POST};
            // ReSharper disable once RedundantAnonymousTypePropertyName
            request.AddJsonBody(new {product = product});
            var response = _client.Execute<Product>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw response.ErrorException;
            }
        }

        public Product UpdateProduct(string sku, Product product, bool saveOptions = true)
        {
            productValidator.ValidateAndThrow(product);

            var request = new RestRequest("products/{sku}");
            request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);
            request.Method = Method.PUT;
            // ReSharper disable once RedundantAnonymousTypePropertyName
            request.AddJsonBody(new {product = product});

            var response = _client.Execute<Product>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw response.ErrorException;
            }
        }

        public void DeleteProduct(string sku)
        {
            var request = new RestRequest("products/{sku}") {Method = Method.DELETE};
            request.AddOrUpdateParameter("sku", sku, ParameterType.UrlSegment);


            var response = _client.Execute<Product>(request);
        }
    }
}