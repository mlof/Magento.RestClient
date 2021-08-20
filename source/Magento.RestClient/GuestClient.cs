using Magento.RestClient.Abstractions;
using RestSharp;

namespace Magento.RestClient
{
	public class GuestClient : IGuestClient
	{
		public GuestClient(IRestClient client)
		{
		}
	}
}