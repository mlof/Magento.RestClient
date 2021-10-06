using Magento.RestClient.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Context
{
	public abstract class BaseContext : IContext
	{
		protected BaseContext(IMemoryCache cache, MemoryCacheOptions memoryCacheOptions)
		{
			if (cache != null)
			{
				this.Cache = cache;
			}
			else
			{
				if (memoryCacheOptions == null)
				{
					memoryCacheOptions = new MemoryCacheOptions();
				}

				this.Cache = new MemoryCache(memoryCacheOptions);
			}
		}

		public abstract IRestClient Client { get; }
		public IMemoryCache Cache { get; }
	}
}