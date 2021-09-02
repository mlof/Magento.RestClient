using Magento.RestClient.Domain.Tests.Constants;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Domain.Tests.Abstractions
{
	public abstract class AbstractDomainObjectTest
	{
		protected IAdminClient Client;
		protected string SimpleProductSku = "TESTSKU";
		protected string VirtualProductSku = "TESTSKU_VIRTUAL";

		[SetUp]
		public void Setup()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
			this.Client = new MagentoClient("http://localhost/").AuthenticateAsIntegration(
				IntegrationCredentials.ConsumerKey, IntegrationCredentials.ConsumerSecret,
				IntegrationCredentials.AccessToken, IntegrationCredentials.AccessTokenSecret);


			var simpleProduct = new Product(this.SimpleProductSku) {
				Name = this.SimpleProductSku, Price = 10, TypeId = ProductType.Simple
			};

			var virtualProduct = new Product(this.VirtualProductSku) {
				Name = this.VirtualProductSku, Price = 10, TypeId = ProductType.Virtual
			};


			this.Client.Products.CreateProduct(simpleProduct);

			this.Client.Products.CreateProduct(virtualProduct);
		}

		[TearDown]
		public void Teardown()
		{
			this.Client.Products.DeleteProduct(SimpleProductSku);
			this.Client.Products.DeleteProduct(VirtualProductSku);
		}
	}
}