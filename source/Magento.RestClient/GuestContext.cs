using Magento.RestClient.Abstractions;
using RestSharp;

namespace Magento.RestClient
{
	public class GuestContext : IGuestContext
	{
		public GuestContext(IRestClient client)
		{
		}
	}
}