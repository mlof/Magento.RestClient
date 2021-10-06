using Magento.RestClient.Configuration;
using Microsoft.Extensions.Configuration;

namespace Magento.RestClient.Tests
{
	public class TestConfiguration
	{

		public static MagentoClientOptions GetInstance()
		{
			var configurationRoot = new ConfigurationBuilder().AddUserSecrets<MagentoClientOptions>().Build();

			var configuration = new MagentoClientOptions();
			configurationRoot.Bind(configuration);
			return configuration;
		}
	}
}