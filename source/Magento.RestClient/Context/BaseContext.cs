using Magento.RestClient.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using Serilog;
using Serilog.Core;

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
		public ILogger Logger => Serilog.Log.Logger.ForContext("BaseUrl", Client.BaseUrl);
	}
}