using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Data.Models.Catalog.Products;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class ProductTests : AbstractAdminTest
    {

        public Product _shouldExist = new Product() {
            // ReSharper disable once StringLiteralTypo
            Sku = "SKU-SHOULDEXIST", 
            
            
            Name = "Should Exist", Price = 30
        };



        private readonly Product _shouldNotExist = new() {
            // ReSharper disable once StringLiteralTypo
            Sku = "SKU-SHOULDNOTEXIST", Name = "Should Be Updated", Price = 30
        };

        [SetUp]
        public void SetupProducts()
        {
            this.Context.Products.CreateProduct(_shouldExist);
        }

        [TearDown]
        public void TeardownProducts()
        {
            this.Context.Products.DeleteProduct("SKU-SHOULDEXIST");
            this.Context.Products.DeleteProduct("SKU-SHOULDBEUPDATED");
            this.Context.Products.DeleteProduct("SKU-SHOULDBECREATED");
        }

        [Test]
        async public Task GetProductBySku_ProductExists()
        {
            var p = await Context.Products.GetProductBySku(_shouldExist.Sku);

            p.Should().NotBeNull();

            p.Name.Should().BeEquivalentTo(_shouldExist.Name);
            p.Sku.Should().BeEquivalentTo(_shouldExist.Sku);
        }

        [Test]
        public void GetProduct_ProductDoesNotExist()
        {
            var p = Context.Products.GetProductBySku(_shouldNotExist.Sku);

            p.Should().BeNull();
        }

        [Test]
        async public Task CreateProduct_WhenProductIsValid()
        {
            var shouldBeCreated = new Product()
            {
                // ReSharper disable once StringLiteralTypo
                Sku = "SKU-SHOULDBECREATED",
                Name = "Should Be Created",
                Price = 30
            };
            var p = await Context.Products.CreateProduct(shouldBeCreated);

            
            p.Name.Should().BeEquivalentTo(shouldBeCreated.Name);
            p.Sku.Should().BeEquivalentTo(shouldBeCreated.Sku);
            
        }

        [Test ]
        async public Task UpdateProduct_WhenProductIsValid()
        {

            
            var updatedTitle = "Has Been Updated";
            _shouldExist.Name = updatedTitle;
            var p = await Context.Products.UpdateProduct(_shouldExist.Sku, _shouldExist);


            p.Name.Should().BeEquivalentTo(updatedTitle);
            p.Sku.Should().BeEquivalentTo(_shouldExist.Sku);
            p.Price.Should().Be(_shouldExist.Price);
        }

		
    }
}