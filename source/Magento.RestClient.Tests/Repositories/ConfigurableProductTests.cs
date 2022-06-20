using Magento.RestClient.Modules.Catalog.Models.Products;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class ConfigurableProductTests : AbstractAdminTest
	{
		public string ParentSku = "CONF-PARENT";
		public string FirstChildSku = "CONF-CHILD-1";
		public string SecondChildSku = "CONF-CHILD-1";


		[SetUp]
		public void SetupConfigurableProducts()
		{

			
			MagentoContext.Products.CreateProduct(new Product() {
				Sku = ParentSku, Price = 0, Name = "Configurable Parent", TypeId = ProductType.Configurable
			});
			MagentoContext.Products.CreateProduct(new Product() {
				Sku = FirstChildSku, Name = "Configurable Child 1", Price = 30, TypeId = ProductType.Simple
			});
			MagentoContext.Products.CreateProduct(new Product() {
				Sku = SecondChildSku, Name = "Configurable Child 2", Price = 30, TypeId = ProductType.Simple
			});
		}

		[Test]
		public void CreateOptions()
		{
			
			//MagentoContext.ConfigurableProducts.CreateOptions();
		}

		[Test]
		public void CreateChild()
		{
			MagentoContext.ConfigurableProducts.CreateChild(ParentSku, FirstChildSku);
		}


		[TearDown]
		public void TeardownConfigurableProducts()
		{
			MagentoContext.Products.DeleteProduct(ParentSku);
			MagentoContext.Products.DeleteProduct(FirstChildSku);
			MagentoContext.Products.DeleteProduct(SecondChildSku);
		}
	}
}