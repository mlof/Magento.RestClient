using FluentAssertions;
using Magento.RestClient.Models;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class ProductTests : AbstractIntegrationTest
    {
        private readonly Product _shouldExist = new() {
            // ReSharper disable once StringLiteralTypo
            Sku = "SKU-SHOULDEXIST", Name = "Should Exist", Price = 30
        };

        private readonly Product _shouldBeCreated = new() {
            // ReSharper disable once StringLiteralTypo
            Sku = "SKU-SHOULDBECREATED", Name = "Should Be Created", Price = 30
        };

        private readonly Product _shouldBeUpdated = new() {
            // ReSharper disable once StringLiteralTypo
            Sku = "SKU-SHOULDBEUPDATED", Name = "Should Be Updated", Price = 30
        };

        private readonly Product _shouldNotExist = new() {
            // ReSharper disable once StringLiteralTypo
            Sku = "SKU-SHOULDNOTEXIST", Name = "Should Be Updated", Price = 30
        };

        [SetUp]
        public void SetupProducts()
        {
            this.Client.Products.CreateProduct(_shouldExist);
            this.Client.Products.CreateProduct(_shouldBeUpdated);
        }

        [TearDown]
        public void TeardownProducts()
        {
            this.Client.Products.DeleteProduct(_shouldExist.Sku);
            this.Client.Products.DeleteProduct(_shouldBeUpdated.Sku);
            this.Client.Products.DeleteProduct(_shouldBeCreated.Sku);
        }

        [Test]
        public void GetProductBySku_ProductExists()
        {
            var p = Client.Products.GetProductBySku(_shouldExist.Sku);

            p.Should().NotBeNull();

            p.Name.Should().BeEquivalentTo(_shouldExist.Name);
            p.Sku.Should().BeEquivalentTo(_shouldExist.Sku);
        }

        [Test]
        public void GetProduct_ProductDoesNotExist()
        {
            var p = Client.Products.GetProductBySku(_shouldNotExist.Sku);

            p.Should().BeNull();
        }

        [Test]
        public void CreateProduct_WhenProductIsValid()
        {
            var p = Client.Products.CreateProduct(_shouldBeCreated);

            
            p.Name.Should().BeEquivalentTo(_shouldBeCreated.Name);
            p.Sku.Should().BeEquivalentTo(_shouldBeCreated.Sku);
            
        }

        [Test ]
        public void UpdateProduct_WhenProductIsValid()
        {
            var updatedTitle = "Has Been Updated";
            _shouldBeUpdated.Name = updatedTitle;
            var p = Client.Products.UpdateProduct(_shouldBeUpdated.Sku, _shouldBeUpdated);


            p.Name.Should().BeEquivalentTo(updatedTitle);
            p.Sku.Should().BeEquivalentTo(_shouldBeUpdated.Sku);
            p.Price.Should().Be(_shouldBeUpdated.Price);
        }
    }
}