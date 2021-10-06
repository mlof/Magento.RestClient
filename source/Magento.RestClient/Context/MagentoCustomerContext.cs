using Magento.RestClient.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Context
{
	public class MagentoCustomerContext : ICustomerContext
	{
		public MagentoCustomerContext()
		{
		}

		public IRestClient Client { get; }
		public IMemoryCache Cache { get; }
	}
}