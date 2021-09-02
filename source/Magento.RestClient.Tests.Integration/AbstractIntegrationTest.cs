using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Tests.Configuration;

using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests.Integration
{
	public abstract class AbstractIntegrationTest
	{
		protected IAdminClient Client;

		[SetUp]
		public void Setup()
		{
			var conf = TestConfiguration.GetInstance();
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
			this.Client = new MagentoClient("http://localhost/").AuthenticateAsIntegration(
				conf.ConsumerKey, conf.ConsumerSecret,
				conf.AccessToken, conf.AccessTokenSecret);
		}


		[TearDown]
		public void Teardown()
		{

		}
	}
}