using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Tests.Constants;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
    public abstract class AbstractIntegrationTest
    {
        protected IAdminClient Client;

        [SetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
            this.Client = new MagentoClient("http://localhost/").AuthenticateAsIntegration(
                IntegrationCredentials.ConsumerKey, IntegrationCredentials.ConsumerSecret,
                IntegrationCredentials.AccessToken, IntegrationCredentials.AccessTokenSecret);
        }
    }
}