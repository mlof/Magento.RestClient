using Magento.RestClient.Models;
using Magento.RestClient.Models.Products;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
	public class ConfigurableProductTests : AbstractIntegrationTest
	{
		public string ParentSku = "CONF-PARENT";
		public string FirstChildSku = "CONF-CHILD-1";
		public string SecondChildSku = "CONF-CHILD-1";


		[SetUp]
		public void SetupConfigurableProducts()
		{

			
			Client.Products.CreateProduct(new Product() {
				Sku = ParentSku, Price = 0, Name = "Configurable Parent", TypeId = ProductType.Configurable
			});
			Client.Products.CreateProduct(new Product() {
				Sku = FirstChildSku, Name = "Configurable Child 1", Price = 30, TypeId = ProductType.Simple
			});
			Client.Products.CreateProduct(new Product() {
				Sku = SecondChildSku, Name = "Configurable Child 2", Price = 30, TypeId = ProductType.Simple
			});
		}

		[Test]
		public void CreateOptions()
		{
			Client.Search.AttributeSets();

			//Client.ConfigurableProducts.CreateOptions();
		}

		[Test]
		public void CreateChild()
		{
			Client.ConfigurableProducts.CreateChild(ParentSku, FirstChildSku);
		}


		[TearDown]
		public void TeardownConfigurableProducts()
		{
			Client.Products.DeleteProduct(ParentSku);
			Client.Products.DeleteProduct(FirstChildSku);
			Client.Products.DeleteProduct(SecondChildSku);
		}
	}
}