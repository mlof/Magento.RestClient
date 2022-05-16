using RestSharp;

namespace Magento.RestClient.Extensions
{
    public static class RestRequestExtensions
    {
        public static RestRequest SetScope(this RestRequest request, string scope)
        {
            request.AddOrUpdateParameter("scope", scope, ParameterType.UrlSegment);
            return request;
        }
    }
}