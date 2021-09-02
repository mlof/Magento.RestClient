using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Abstractions
{
    public interface IMagentoClient
    {
        IAdminClient AuthenticateAsIntegration(string consumerKey, string consumerSecret, string accessToken,
            string accessTokenSecret);
        IAdminClient AuthenticateAsAdmin(string username, string password);
        ICustomerClient AuthenticateAsCustomer(string username, string password);

        IGuestClient AuthenticateAsGuest();
    }
}