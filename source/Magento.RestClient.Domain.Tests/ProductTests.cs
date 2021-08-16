using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class ProductTests : AbstractDomainObjectTest
	{
		public string Sku = "TESTSKU";

		[SetUp]
		public void ProductSetup()
		{
		}


		[Test]
		public void CreateProduct()
		{
			var productFactory = ProductFactory.CreateInstance(Client);
			var product = productFactory.CreateNew(Sku);

			product.SetName("TestProduct");

			product.SetVisibility(ProductVisibility.Both);
			product.SetPrice(50);
		}


		[TearDown]
		public void ProductTeardown()
		{
			Client.Products.DeleteProduct(Sku);
		}
	}
}