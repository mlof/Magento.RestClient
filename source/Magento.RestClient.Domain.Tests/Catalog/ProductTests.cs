using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Domain.Models.Catalog;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class ProductTests : AbstractAdminTest
	{
		[SetUp]
		async public Task ProductSetup()
		{
		}


		[Test]
		async public Task CreateSimpleProduct()
		{
			var model = new ProductModel(Context, "TESTPRODUCT");
			model.Price = 10;

			await model.SaveAsync();

			var getResult = await Context.Products.GetProductBySku("TESTPRODUCT");


			Assert.NotNull(getResult);
			if (getResult != null)
			{
				Assert.Equals(getResult.AttributeSetId, 4);
				Assert.Equals(getResult.Sku, "TESTPRODUCT");
				Assert.Equals(getResult.Name, "TESTPRODUCT");
				Assert.Equals(getResult.Visibility, 4);
				Assert.Equals(getResult.TypeId, ProductType.Simple);
				Assert.Equals(getResult.Price, 10);
			}
		}

		[Test]
		public async Task CreateProduct_WithOptions()
		{
			var model = new ProductModel(Context, "TESTPRODUCT")
			{
				Price = 10,
				Options = new List<ProductOption>() {
					new() {
						Title = "Option",
						Type = ProductOptionType.DropDown,
						Values = new List<ProductOptionValue>() {
							new("100", 100),
							new("200", 200)
						}
					}
				}
			};

			await model.SaveAsync();

			var getResult = await Context.Products.GetProductBySku("TESTPRODUCT");

			Assert.That(getResult.Options.Count == 1);
		}


		[Test]
		public void GetExistingProduct()
		{
		}


		[TearDown]
		public async Task TeardownProducts()
		{
			await Context.Products.DeleteProduct("TESTPRODUCT");
		}
	}
}