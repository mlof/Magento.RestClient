using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using Magento.RestClient.Models.Products;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class ProductTests : AbstractDomainObjectTest
	{

		[SetUp]
		public void ProductSetup()
		{
		}


		[Test]
		public void CreateSimpleProduct()
		{
			var sku = "TEST-SIMPLEPRODUCT";
			var product = new ProductModel(this.Context, sku);

			product.Name = "TestProduct";
			product.AttributeSetId = 28;

			product.Visibility = ProductVisibility.Both;
			product.Price = 50;

			product.SetAttribute("test_attr", "Xyz");


			product.Save();
		}

		[Test]
		public void CreateConfigurableProduct()
		{
			var sku = "TEST-CONFIGURABLEPRODUCT";
			var product = new ProductModel(this.Context, sku);
			product.Name = "TestProduct";
			product.AttributeSetId = 28;
			product.Visibility = ProductVisibility.Both;
			product.Price = 50;
			product.SetAttribute("test_attr", "Xyz");

			

			product.Save();

			var persistedProduct= new ProductModel(this.Context, sku);

			product.Name.Should().BeEquivalentTo("TestProduct");

		}

		[Test]
		public void GetExistingProduct()
		{
			var product = Context.GetProductModel(SimpleProductSku);

			product.Name.Should().NotBeNullOrWhiteSpace();
		}

		[TearDown]
		public void ProductTeardown()
		{
			Context.Products.DeleteProduct("TEST-SIMPLEPRODUCT");
		}
	}
}