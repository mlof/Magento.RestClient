using Magento.RestClient.Models;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class ConfigurableProductTests : AbstractIntegrationTest
    {
        public Product Parent => new Product() {
            Sku = "CONF-PARENT", 
            Price = 0,
            Name = "Configurable Parent",
            TypeId = ProductType.Configurable
        };

        public Product Child => new Product() {
            Sku = "CONF-CHILD-1" , 
            Name = "Configurable Child",

            Price = 30, 
            TypeId =  ProductType.Simple
        };

        [SetUp]
        public void SetupConfigurableProducts()
        {
            Client.Products.CreateProduct(Parent);
            Client.Products.CreateProduct(Child);
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
            Client.ConfigurableProducts.CreateChild(Parent.Sku, Parent.Sku);
        }


        [TearDown]
        public void TeardownConfigurableProducts()
        {
            Client.Products.DeleteProduct(Parent.Sku);
        }
    }
}