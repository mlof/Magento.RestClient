using Magento.RestClient.Domain.Tests.Constants;
using Magento.RestClient.Repositories.Abstractions;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Domain.Tests.Abstractions
{
    public abstract class AbstractDomainObjectTest
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