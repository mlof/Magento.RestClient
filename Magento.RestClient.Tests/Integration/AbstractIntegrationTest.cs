using MagentoApi.Repositories.Abstractions;
using MagentoApi.Tests.Constants;
using NUnit.Framework;
using Serilog;

namespace MagentoApi.Tests.Integration
{
    public abstract class AbstractIntegrationTest
    {
        protected IIntegrationClient client;

        [SetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
            this.client = new MagentoClient("http://localhost/rest/V1/").AuthenticateAsIntegration(
                IntegrationCredentials.ConsumerKey, IntegrationCredentials.ConsumerSecret,
                IntegrationCredentials.AccessToken, IntegrationCredentials.AccessTokenSecret);
            ;
        }
    }
}