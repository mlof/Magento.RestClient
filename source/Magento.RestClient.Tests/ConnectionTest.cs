using System.Linq;
using NUnit.Framework;

namespace Magento.RestClient.Tests
{
	public class ConnectionTest
	{
		[Test]
		public void TestConnection()
		{

			var magentoClient = new MagentoClient("http://localhost");
			var context = magentoClient.AuthenticateAsAdmin("user", "bitnami1");

			var products = context.Products.AsQueryable().Take(10).ToList();
		}
	}
}