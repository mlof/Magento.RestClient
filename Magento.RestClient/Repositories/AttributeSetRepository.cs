using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class AttributeSetRepository : IAttributeSetRepository
    {
        private readonly IRestClient _client;

        public AttributeSetRepository(IRestClient client)
        {
            this._client = client;
        }
    }
}