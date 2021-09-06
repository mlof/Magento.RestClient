using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Tests.Configuration;
using Magento.RestClient.Tests.Constants;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
	public class ConnectionTest
	{
		[Test]
		public void TestConnection()
		{

			var magentoClient = new MagentoClient("http://localhost");
			var context = magentoClient.AuthenticateAsAdmin("user", "bitnami1");

			var products = context.Search.Products(builder => builder.WithPage(1).WithPageSize(10)).Items;
		}
	}
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