using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Abstractions
{
	public interface IMagentoClient
	{
		IAdminContext AuthenticateAsIntegration(string consumerKey, string consumerSecret, string accessToken,
			string accessTokenSecret);

		IAdminContext AuthenticateAsAdmin(string username, string password);
		ICustomerContext AuthenticateAsCustomer(string username, string password);

		IGuestContext AuthenticateAsGuest();
	}
}