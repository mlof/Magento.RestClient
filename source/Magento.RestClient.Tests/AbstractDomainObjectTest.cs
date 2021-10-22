using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
	public abstract class AbstractAdminTest
	{
		protected IAdminContext Context;

		[SetUp]
		public void Setup()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

			var configurationRoot = new ConfigurationBuilder().AddUserSecrets<MagentoClientOptions>().Build();

			var configuration = new MagentoClientOptions();
			configurationRoot.Bind(configuration);

			this.Context = new MagentoAdminContext(configuration);
			var consumerKey = "";
			var consumerSecret = "";
			var accessToken = "";
			var accessTokenSecret = "";

			var context = new MagentoAdminContext("https://magento.localhost", consumerKey, consumerSecret, accessToken, accessTokenSecret);

			
		}

		[TearDown]
		public void Teardown()
		{
		}
	}
}