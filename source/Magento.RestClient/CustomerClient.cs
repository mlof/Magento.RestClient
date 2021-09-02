using Magento.RestClient.Abstractions;
using RestSharp;

namespace Magento.RestClient
{
	public class CustomerClient : ICustomerClient
	{
		public CustomerClient(IRestClient client)
		{
		}
	}
}