using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Magento.RestClient.Abstractions.Abstractions
{
	public interface IContext
	{
		RestSharp.RestClient RestClient { get; }
		IMemoryCache Cache { get; }
		ILogger Logger { get; }
	}
}