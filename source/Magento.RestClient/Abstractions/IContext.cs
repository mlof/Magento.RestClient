using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using Serilog;

namespace Magento.RestClient.Abstractions
{
	public interface IContext
	{
		IRestClient Client { get;  }
		IMemoryCache Cache { get;  }
		ILogger Logger { get; }
	}
}