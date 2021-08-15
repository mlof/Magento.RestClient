using System.Collections.Generic;
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
        public static Product Parent = new Product() {
            Sku = "CONF-PARENT", Price = 0, Name = "Configurable Parent", TypeId = ProductType.Configurable
        };

        public static Product FirstChild = new Product() {
            Sku = "CONF-CHILD-1", Name = "First Configurable Child", Price = 30, TypeId = ProductType.Simple
        };

        public static Product SecondChild = new Product() {
            Sku = "CONF-CHILD-2", Name = "Second Configurable Child", Price = 30, TypeId = ProductType.Simple
        };

        [SetUp]
        public void SetupProducts()
        {
            Client.AttributeSets.Create(EntityType.CatalogProduct, 4,
                new AttributeSet() {AttributeSetName = "Test Attribute Set"});

            // ReSharper disable once PossibleNullReferenceException
            var attributeSetId = Client.Search.AttributeSets(builder =>
                    builder.Where(set => set.AttributeSetName, SearchCondition.Equals, "Test Attribute Set"))
                .Items
                .SingleOrDefault()
                .AttributeSetId;

            var x = Client.Attributes.GetProductAttributes(attributeSetId);

            var testAttribute = new ProductAttribute("testattribute", "select") {
                DefaultFrontendLabel = "Test Attribute",
                FrontendLabels =
                    new List<AttributeLabel>() {new AttributeLabel() {StoreId = 1, Label = "Test Attribute"}}
            };
            testAttribute = Client.Attributes.Create(testAttribute);


            var options = Client.Attributes.GetProductAttributeOptions("testattribute");

            var abc = Client.Attributes.CreateProductAttributeOption("testattribute",
                new Option() {Label = "ABC", Value = "abc"});


            var def = Client.Attributes.CreateProductAttributeOption("testattribute",
                new Option() {Label = "DEF", Value = "def"});


            var attributeGroup = Client.AttributeSets.CreateProductAttributeGroup(attributeSetId, "attributeGroupName");


            Client.AttributeSets.AssignProductAttribute(attributeSetId, attributeGroup, "testattribute");

            Parent.AttributeSetId = attributeSetId;
            FirstChild.AttributeSetId = attributeSetId;
            SecondChild.AttributeSetId = attributeSetId;

            FirstChild.CustomAttributes = new List<CustomAttribute>() {
                new CustomAttribute() {AttributeCode = "testattribute", Value = abc.ToString()}
            };
            SecondChild.CustomAttributes = new List<CustomAttribute>() {
                new CustomAttribute() {AttributeCode = "testattribute", Value = def.ToString()}
            };
            Client.Products.CreateProduct(Parent);
            Client.Products.CreateProduct(FirstChild);
            Client.Products.CreateProduct(SecondChild);

            Client.ConfigurableProducts.CreateOption(Parent.Sku, testAttribute.AttributeId, abc, "abc");


            Client.ConfigurableProducts.CreateChild(Parent.Sku, FirstChild.Sku);
            Client.ConfigurableProducts.CreateChild(Parent.Sku, SecondChild.Sku);
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
            Client.Products.DeleteProduct(FirstChild.Sku);
            Client.Products.DeleteProduct(SecondChild.Sku);

            // ReSharper disable once PossibleNullReferenceException
            var attributeSetId = Client.Search.AttributeSets(builder =>
                    builder.Where(set => set.AttributeSetName, SearchCondition.Equals, "Test Attribute Set"))
                .Items
                .SingleOrDefault()
                .AttributeSetId;

            Client.Attributes.DeleteProductAttribute("testattribute");
            Client.AttributeSets.Delete(attributeSetId);
        }
    }
}