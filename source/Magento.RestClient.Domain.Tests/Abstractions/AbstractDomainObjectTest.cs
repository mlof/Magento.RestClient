using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Tests.Constants;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Domain.Tests.Abstractions
{
	public abstract class AbstractDomainObjectTest
	{
		protected IAdminContext Context;
		protected string SimpleProductSku = "TESTSKU";
		protected string VirtualProductSku = "TESTSKU_VIRTUAL";

		[SetUp]
		public void Setup()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
			this.Context = new MagentoClient("http://localhost/").AuthenticateAsIntegration(
				IntegrationCredentials.ConsumerKey, IntegrationCredentials.ConsumerSecret,
				IntegrationCredentials.AccessToken, IntegrationCredentials.AccessTokenSecret);


			var simpleProduct = new Product(this.SimpleProductSku) {
				Name = this.SimpleProductSku, Price = 10, TypeId = ProductType.Simple
			};

			var virtualProduct = new Product(this.VirtualProductSku) {
				Name = this.VirtualProductSku, Price = 10, TypeId = ProductType.Virtual
			};


			this.Context.Products.CreateProduct(simpleProduct);

			this.Context.Products.CreateProduct(virtualProduct);
		}

		[TearDown]
		public void Teardown()
		{
			this.Context.Products.DeleteProduct(SimpleProductSku);
			this.Context.Products.DeleteProduct(VirtualProductSku);
		}
	}
}