using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Tests.Configuration;
using Magento.RestClient.Tests.Constants;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
    public abstract class AbstractIntegrationTest
    {
        protected IAdminContext Context;

        [SetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

			var conf = TestConfiguration.GetInstance();
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
			this.Context = new MagentoClient("http://localhost/").AuthenticateAsIntegration(
				conf.ConsumerKey, conf.ConsumerSecret,
				conf.AccessToken, conf.AccessTokenSecret);
		}
    }
}