using Magento.RestClient.Search.Abstractions;
using RestSharp;

namespace Magento.RestClient.Search
{
    public static class ClientExtensions
    {
        public static ISearchService Search(this IRestClient restClient)
        {
            return new SearchService(restClient);
        }
    }
}