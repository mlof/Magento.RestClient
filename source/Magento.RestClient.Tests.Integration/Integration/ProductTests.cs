using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Magento.RestClient.Domain.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public class ProductTests: AbstractIntegrationTest
	{
		[SetUp]
		public void ProductsSetup()
		{
			var products = File.ReadAllText(Path.Join("Fixtures", "products.json"));
			this.ProductFixtures= JsonConvert.DeserializeObject<List<ProductFixture>>(products);

		}


		[Test]
		public void Xyz()
		{

			foreach (var productFixture in this.ProductFixtures.Where(fixture => fixture.Title.Contains("Asus", StringComparison.InvariantCultureIgnoreCase)))
			{
				var product = Client.GetProductModel(productFixture.Sku);


				product.SetStock(productFixture.InStock);
				
				product.Save();
				
			}
		}
		public List<ProductFixture>? ProductFixtures { get; set; }
	}
}