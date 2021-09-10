using Magento.RestClient.Data.Models.Products;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class ConfigurableProductTests : AbstractIntegrationTest
	{
		public string ParentSku = "CONF-PARENT";
		public string FirstChildSku = "CONF-CHILD-1";
		public string SecondChildSku = "CONF-CHILD-1";


		[SetUp]
		public void SetupConfigurableProducts()
		{

			
			Context.Products.CreateProduct(new Product() {
				Sku = ParentSku, Price = 0, Name = "Configurable Parent", TypeId = ProductType.Configurable
			});
			Context.Products.CreateProduct(new Product() {
				Sku = FirstChildSku, Name = "Configurable Child 1", Price = 30, TypeId = ProductType.Simple
			});
			Context.Products.CreateProduct(new Product() {
				Sku = SecondChildSku, Name = "Configurable Child 2", Price = 30, TypeId = ProductType.Simple
			});
		}

		[Test]
		public void CreateOptions()
		{
			
			//Context.ConfigurableProducts.CreateOptions();
		}

		[Test]
		public void CreateChild()
		{
			Context.ConfigurableProducts.CreateChild(ParentSku, FirstChildSku);
		}


		[TearDown]
		public void TeardownConfigurableProducts()
		{
			Context.Products.DeleteProduct(ParentSku);
			Context.Products.DeleteProduct(FirstChildSku);
			Context.Products.DeleteProduct(SecondChildSku);
		}
	}
}