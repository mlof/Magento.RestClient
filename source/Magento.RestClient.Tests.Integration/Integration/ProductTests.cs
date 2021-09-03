using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bogus;
using Magento.RestClient.Domain.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public class ProductTests: AbstractIntegrationTest
	{
		private List<ProductFixture> products_100;

		[SetUp]
		public void ProductsSetup()
		{
			
			this.Faker = new Faker<ProductFixture>()
				.RuleFor(product => product.Sku, faker => faker.Commerce.Ean13())
				.RuleFor(fixture => fixture.Price, faker => Convert.ToDecimal(faker.Commerce.Price()))
				.RuleFor(fixture => fixture.InStock, faker => faker.Random.Number(0, 900))
				.RuleFor(fixture => fixture.Title, faker => faker.Commerce.Product())
				;
			this.products_100 = Faker.Generate(100);

		}

		public Faker<ProductFixture> Faker { get; set; }


		[Test]
		public void Create_100_Products()
		{
			
			foreach (var product in products_100)
			{
				var p = Context.GetProductModel(product.Sku);
				p.AttributeSetId = 4;
				p.Name = product.Title;
				p.Price = product.Price;
				p.SetStock(product.InStock);
				p.Save();
			}
			/*
			foreach (var productFixture in this.ProductFixtures.Where(fixture => fixture.Title.Contains("Asus", StringComparison.InvariantCultureIgnoreCase)))
			{
				var product = Context.GetProductModel(productFixture.Sku);


				product.SetStock(productFixture.InStock);
				
				product.Save();
				
			}*/
		}
	}
}