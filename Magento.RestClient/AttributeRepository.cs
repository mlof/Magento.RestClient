using System.Collections.Generic;
using Magento.RestClient.Repositories;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient
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

        public void Create(ProductAttribute attribute)
        {
            var request = new RestRequest("products/attributes");

            request.Method = Method.POST;
            request.AddJsonBody(new {attribute});


            _client.Execute(request);
        }

        public void DeleteProductAttribute(string attributeCode)
        {
            var request = new RestRequest("products/attributes/{attributeCode}");

            request.Method = Method.DELETE;
            request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
            _client.Execute(request);
        }
    }
}