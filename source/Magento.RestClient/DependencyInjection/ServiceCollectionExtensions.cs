using System;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Magento.RestClient.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		
		public static void AddMagentoAdminClient(this IServiceCollection services,
			Action<MagentoClientOptions> configureOptions = null)
		{
			if (configureOptions != null)
			{
				services.Configure(configureOptions);
			}

			services.TryAddSingleton<MagentoContext>();
		}

	}
}