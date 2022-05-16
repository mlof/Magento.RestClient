using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Magento.RestClient.Context
{
	public abstract class BaseContext
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

		public IMemoryCache Cache { get; }
		public ILogger Logger => Log.Logger;
	}
}