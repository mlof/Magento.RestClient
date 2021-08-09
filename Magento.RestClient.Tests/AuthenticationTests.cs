using MagentoApi.Tests.Constants;
using NUnit.Framework;

namespace MagentoApi.Tests
{
    public class AuthenticationTests
    {
        private MagentoClient client;

        [SetUp]
        public void Setup()
        {
            this.client = new MagentoClient("http://localhost/rest/V1/");
        }

        [Test]
        public void CanAuthenticateAsIntegration()
        {
            var c = client.AuthenticateAsIntegration(IntegrationCredentials.ConsumerKey,
                IntegrationCredentials.ConsumerSecret,
                IntegrationCredentials.AccessToken, IntegrationCredentials.AccessTokenSecret);

            var websites = c.Stores.GetWebsites();

            Assert.IsNotEmpty(websites);
        }
    }
}