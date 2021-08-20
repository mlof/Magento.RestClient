using Magento.RestClient.Tests.Constants;
using NUnit.Framework;

namespace Magento.RestClient.Tests
{
	public class AuthenticationTests
	{
		private MagentoClient _client;

		[SetUp]
		public void Setup()
		{
			this._client = new MagentoClient("http://localhost/");
		}

		[Test]
		public void CanAuthenticateAsIntegration()
		{
			var c = _client.AuthenticateAsIntegration(IntegrationCredentials.ConsumerKey,
				IntegrationCredentials.ConsumerSecret,
				IntegrationCredentials.AccessToken, IntegrationCredentials.AccessTokenSecret);

			var websites = c.Stores.GetWebsites();

			Assert.IsNotEmpty(websites);
		}

		[Test]
		public void CanAuthenticateAsAdmin()
		{
			var c = _client.AuthenticateAsAdmin(@"user", "bitnami1");


			var websites = c.Stores.GetWebsites();

			Assert.IsNotEmpty(websites);
		}


		[Test]
		public void CanAuthenticateAsCustomer()
		{
			var c = _client.AuthenticateAsCustomer(@"user", "bitnami1");
		}
	}
}