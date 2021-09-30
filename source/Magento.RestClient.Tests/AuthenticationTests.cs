using System.Threading.Tasks;
using Magento.RestClient.Tests.Configuration;
using Magento.RestClient.Tests.Constants;
using NUnit.Framework;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;

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
		async public Task CanAuthenticateAsIntegration()
		{
			var conf = TestConfiguration.GetInstance();
			var c = new MagentoClient("http://localhost/").AuthenticateAsIntegration(
				conf.ConsumerKey, conf.ConsumerSecret,
				conf.AccessToken, conf.AccessTokenSecret);


			var websites = await c.Stores.GetWebsites();

			Assert.IsNotEmpty(websites);
		}

		[Test]
		async public Task CanAuthenticateAsAdmin()
		{
			var c = _client.AuthenticateAsAdmin(@"user", "bitnami1");


			var websites = await c.Stores.GetWebsites();

			Assert.IsNotEmpty(websites);
		}


		[Test]
		public void CanAuthenticateAsCustomer()
		{
			var c = _client.AuthenticateAsCustomer(@"user", "bitnami1");
		}
	}
}