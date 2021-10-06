using Magento.RestClient.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Context
{
	public class MagentoGuestContext : IGuestContext
	{
		public MagentoGuestContext()
		{
		}

		public IRestClient Client { get; }
		public IMemoryCache Cache { get; }
	}
}