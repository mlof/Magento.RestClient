using System.Collections.Generic;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    internal class ConfigurableProductRepository : AbstractRepository, IConfigurableProductRepository
    {
        private readonly IRestClient client;

        public ConfigurableProductRepository(IRestClient client)
        {
            this.client = client;
        }

        public void CreateChild(string parentSku, string childSku)
        {
            var request = new RestRequest("configurable-products/{sku}/child");
            request.Method = Method.POST;
            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            request.AddJsonBody(new {childSku});

            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw MagentoException.Parse(response.Content);
            }
        }

        public void DeleteChild(string parentSku, string childSku)
        {
            var request = new RestRequest("configurable-products/{sku}/child/{childSku}");
            request.Method = Method.DELETE;
            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            request.AddOrUpdateParameter("childSku", childSku);

            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw MagentoException.Parse(response.Content);
            }
        }

        public List<ConfigurableProduct> GetConfigurableChildren(string parentSku)
        {
            var request = new RestRequest("configurable-products/{sku}/children");
            request.Method = Method.GET;

            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);

            var response = client.Execute<List<ConfigurableProduct>>(request);
			return HandleResponse(response);

		}

		public void CreateOption(string parentSku, long attributeId, int valueId, string label)
        {
            var request = new RestRequest("configurable-products/{sku}/options");
            request.Method = Method.POST;
            request.AddJsonBody(new {
                option = new {attribute_id = attributeId, 
                    label = label,
                    values = new List<object>() {new {value_index = valueId}}}
            });
            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            client.Execute(request);
        }
    }
}