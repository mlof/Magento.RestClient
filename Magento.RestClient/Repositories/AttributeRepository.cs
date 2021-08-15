using System.Collections.Generic;
using Magento.RestClient.Domain;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    internal class AttributeRepository : IAttributeRepository
    {
        private readonly IRestClient _client;

        public AttributeRepository(IRestClient client)
        {
            this._client = client;
        }


        public IEnumerable<EntityAttribute> GetProductAttributes(long attributeSetId)
        {
            var request = new RestRequest("products/attribute-sets/{id}/attributes");
            request.Method = Method.GET;
            request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);

            var response = _client.Execute<List<EntityAttribute>>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }

            else
            {
                throw MagentoException.Parse(response.Content);
            }
        }

        public ProductAttribute Create(ProductAttribute attribute)
        {
            var request = new RestRequest("products/attributes");

            request.Method = Method.POST;
            request.AddJsonBody(new {attribute});


            var response = _client.Execute<ProductAttribute>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw MagentoException.Parse(response.Content);
            }
        }

        public void DeleteProductAttribute(string attributeCode)
        {
            var request = new RestRequest("products/attributes/{attributeCode}");

            request.Method = Method.DELETE;
            request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
            _client.Execute(request);
        }

        public List<Option> GetProductAttributeOptions(string attributeCode)
        {
            var request = new RestRequest("products/attributes/{attributeCode}/options");

            request.Method = Method.GET;
            request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
            var response = _client.Execute<List<Option>>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw MagentoException.Parse(response.Content);
            }
        }

        public int CreateProductAttributeOption(string attributeCode, Option option)
        {
            var request = new RestRequest("products/attributes/{attributeCode}/options");

            request.Method = Method.POST;
            request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
            request.AddJsonBody(new {option});
            var response = _client.Execute<int>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                throw MagentoException.Parse(response.Content);
            }
        }
    }
}