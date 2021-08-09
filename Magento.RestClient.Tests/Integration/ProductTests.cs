using FluentAssertions;
using MagentoApi.Models;
using NUnit.Framework;

namespace MagentoApi.Tests.Integration
{
    public class ProductTests : AbstractIntegrationTest
    {
        private Product shouldExist = new Product()
        {
            Sku = "SKU-SHOULDEXIST",
            Name = "Should Exist",
            Price = 30
        };
        private Product shouldBeCreated = new Product()
        {
            Sku = "SKU-SHOULDBECREATED",
            Name = "Should Be Created",
            Price = 30
        };
        private Product shouldBeUpdated = new Product()
        {
            Sku = "SKU-SHOULDBEUPDATED",
            Name = "Should Be Updated",
            Price = 30
        };

        [SetUp]
        public void SetupProducts()
        {
            this.client.Products.CreateProduct(shouldExist);
            this.client.Products.CreateProduct(shouldBeUpdated);
        }

        [TearDown]
        public void TeardownProducts()
        {
            this.client.Products.DeleteProduct(shouldExist.Sku);
            this.client.Products.DeleteProduct(shouldBeUpdated.Sku);
            this.client.Products.DeleteProduct(shouldBeCreated.Sku);
        }

        [Test]
        public void CanGetProduct()
        {
            var p = client.Products.GetProductBySku(shouldExist.Sku);

            p.Name.Should().BeEquivalentTo(shouldExist.Name);
            p.Sku.Should().BeEquivalentTo(shouldExist.Sku);
        }

        [Test]
        public void CanCreateProduct()
        {
            var p = client.Products.CreateProduct(shouldBeCreated);

            p.Name.Should().BeEquivalentTo(shouldBeCreated.Name);
            p.Sku.Should().BeEquivalentTo(shouldBeCreated.Sku);


        }

        [Test]
        public void UpdateProduct()
        {
            var updatedTitle = "Has Been Updated";
            shouldBeUpdated.Name = updatedTitle;
            var p = client.Products.UpdateProduct(shouldBeUpdated.Sku, shouldBeUpdated);
            

            p.Name.Should().BeEquivalentTo(updatedTitle);
            p.Sku.Should().BeEquivalentTo(shouldBeUpdated.Sku);
            p.Price.Should().Be(shouldBeUpdated.Price);


        }
    }
}