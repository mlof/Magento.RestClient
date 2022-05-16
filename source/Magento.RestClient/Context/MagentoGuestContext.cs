using Magento.RestClient.Abstractions.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Magento.RestClient.Context
{
	public class MagentoGuestContext : IGuestContext
	{
		public RestSharp.RestClient RestClient { get; }
		public IMemoryCache Cache { get; }
		public ILogger Logger { get; }
	}
}