using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Abstractions
{
	public interface IContext
	{
		IRestClient Client { get;  }
		IMemoryCache Cache { get;  }
	}
}