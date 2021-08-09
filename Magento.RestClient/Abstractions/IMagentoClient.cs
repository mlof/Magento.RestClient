using MagentoApi.Models;
using MagentoApi.Repositories.Abstractions;

namespace MagentoApi.Abstractions
{
    public interface IMagentoClient
    {
        IIntegrationClient AuthenticateAsIntegration(string consumerKey, string consumerSecret, string accessToken,
            string accessTokenSecret);
        void AuthenticateAsAdmin(string username, string password);
        void AuthenticateAsCustomer(string username, string password);
  
    }
}