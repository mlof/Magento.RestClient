using System.Linq;
using FluentAssertions;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class AttributeSetTests : AbstractIntegrationTest
    {
        [SetUp]
        public void AttributeSetSetup()
        {
            var defaultProductAttributeSet = 4;

            var laptopAttributeSet = new AttributeSet() {
                EntityTypeId = EntityType.CatalogProduct, AttributeSetName = "Laptops"
            };

            Client.AttributeSets.Create(EntityType.CatalogProduct, defaultProductAttributeSet, laptopAttributeSet);
        }


        [TearDown]
        public void AttributeSetTearDown()
        {
            var response = Client.Search.AttributeSets(builder => builder
                .WithPageSize(100)
                .WithPage(1)
                .Where(set => set.EntityTypeId, SearchCondition.Equals, EntityType.CatalogProduct)
                .Where(set => set.AttributeSetName, SearchCondition.Equals, "Laptops"));

            var attributeSet = response.Items.First();
            Client.AttributeSets.Delete(attributeSet.AttributeSetId);
        }

        [Test]
        public void Search_Existing()
        {
            var response = Client.Search.AttributeSets(builder => builder
                .WithPageSize(100)
                .WithPage(1)
                .Where(set => set.EntityTypeId, SearchCondition.Equals, EntityType.CatalogProduct)
                .Where(set => set.AttributeSetName, SearchCondition.Equals, "Laptops"));

            response.Items.Should().HaveCount(1);
        }

        [Test]
        public void Search_NonExistent()
        {
            var response = Client.Search.AttributeSets(builder => builder
                .WithPageSize(100)
                .WithPage(1)
                .Where(set => set.EntityTypeId, SearchCondition.Equals, EntityType.CatalogProduct)
                .Where(set => set.AttributeSetName, SearchCondition.Equals, "Hamsters"));

            response.Items.Should().HaveCount(0);
        }

        [Test]
        public void GetProductAttributes_Existent()
        {
            var response = Client.Search.AttributeSets(builder => builder
                .WithPageSize(100)
                .WithPage(1)
                .Where(set => set.EntityTypeId, SearchCondition.Equals, EntityType.CatalogProduct));

            var attributeSet = response.Items.First();


            var attributes =  Client.Attributes.GetProductAttributes(attributeSet.AttributeSetId);

            attributes.Should().NotBeEmpty();
        }
        [Test]
        public void GetProductAttributeGroups_Existent()
        {
            var response = Client.Search.AttributeSets(builder => builder
                .WithPageSize(100)
                .WithPage(1)
                .Where(set => set.EntityTypeId, SearchCondition.Equals, EntityType.CatalogProduct));

            var attributeSet = response.Items.First();


            //var groups = Client.Search.AttributeSets(attributeSet.AttributeSetId);

            //attributes.Should().NotBeEmpty();
        }
        [Test]
        public void GetProductAttributes_NonExistent()
        {
            

            var attributes =  Client.Attributes.GetProductAttributes(-1);
            attributes.Should().BeNull();

        }
    }

    public class AttributeTests : AbstractIntegrationTest
    {

    }
}