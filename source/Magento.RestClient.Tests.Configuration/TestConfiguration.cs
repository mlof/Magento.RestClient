using System;
using Microsoft.Extensions.Configuration;

namespace Magento.RestClient.Tests.Configuration
{
	public class TestConfiguration
	{
		public string ConsumerKey { get; set; }
		public string ConsumerSecret { get; set; }
		public string AccessTokenSecret { get; set; }
		public string AccessToken { get; set; }

		public static TestConfiguration GetInstance()
		{
			var configurationRoot = new ConfigurationBuilder().AddUserSecrets<TestConfiguration>().Build();

			var configuration = new TestConfiguration();
			configurationRoot.Bind(configuration);
			return configuration;
		}
	}
}