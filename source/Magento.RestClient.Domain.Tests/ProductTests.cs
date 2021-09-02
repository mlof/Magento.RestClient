using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using Magento.RestClient.Models.Products;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class ProductTests : AbstractDomainObjectTest
	{
		public string Sku = "NEWSKU";

		[SetUp]
		public void ProductSetup()
		{
		}


		[Test]
		public void CreateProduct()
		{
			var product = new ProductModel(this.Client, Sku);

			product.Name = "TestProduct";
			product.AttributeSetId = 28;

			product.Visibility = ProductVisibility.Both;
			product.Price = 50;

			product.SetAttribute("test_attr", "Xyz");


			product.Save();
		}


		[TearDown]
		public void ProductTeardown()
		{
			Client.Products.DeleteProduct(Sku);
		}
	}
}