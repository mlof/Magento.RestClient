using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Abstractions
{
    public interface IMagentoClient
    {
        IIntegrationClient AuthenticateAsIntegration(string consumerKey, string consumerSecret, string accessToken,
            string accessTokenSecret);
        void AuthenticateAsAdmin(string username, string password);
        void AuthenticateAsCustomer(string username, string password);
  
    }
}