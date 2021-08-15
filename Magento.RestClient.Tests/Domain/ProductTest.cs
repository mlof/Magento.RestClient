using System.Linq;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using Magento.RestClient.Tests.Integration;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
    public class ProductTest : AbstractIntegrationTest
    {

        public Product Parent => new Product()
        {
            Sku = "CONF-PARENT",
            Price = 0,
            Name = "Configurable Parent",
            TypeId = ProductType.Configurable
        };

        public Product Child => new Product()
        {
            Sku = "CONF-CHILD-1",
            Name = "Configurable Child",

            Price = 30,
            TypeId = ProductType.Simple
        };

        [SetUp]
        public void SetupProducts()
        {
            Client.AttributeSets.Create(EntityType.CatalogProduct, 4, new AttributeSet()
            {
                AttributeSetName = "Test Attribute Set"
            });

            // ReSharper disable once PossibleNullReferenceException
            var attributeSetId =  Client.Search.AttributeSets(builder =>
                builder.Where(set => set.AttributeSetName, SearchCondition.Equals, "Test Attribute Set"))
                .Items
                .SingleOrDefault()
                .AttributeSetId;

            var x = Client.AttributeSets.GetProductAttributes(attributeSetId);

            Parent.AttributeSetId = attributeSetId;
            Child.AttributeSetId = attributeSetId;
            Client.Products.CreateProduct(Parent);
            Client.Products.CreateProduct(Child);
        }

        [Test]
        public void Create()
        {

            var x = 9;
        }



        [TearDown]
        public void TeardownConfigurableProducts()
        {
            Client.Products.DeleteProduct(Parent.Sku);
            Client.Products.DeleteProduct(Child.Sku);

            // ReSharper disable once PossibleNullReferenceException
            var attributeSetId = Client.Search.AttributeSets(builder =>
                    builder.Where(set => set.AttributeSetName, SearchCondition.Equals, "Test Attribute Set"))
                .Items
                .SingleOrDefault()
                .AttributeSetId;

            Client.AttributeSets.Delete(attributeSetId);
        }
    }
}