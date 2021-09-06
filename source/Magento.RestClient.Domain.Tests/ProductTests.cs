using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class ProductTests : AbstractDomainObjectTest
	{
		[SetUp]
		public void ProductSetup()
		{
			var sizeAttribute = Context.GetAttributeModel("monitor_sizes");
			sizeAttribute.DefaultFrontendLabel = "Monitor Size";
			sizeAttribute.FrontendInput = "select";
			sizeAttribute.AddOption("13 inch");
			sizeAttribute.AddOption("14 inch");
			sizeAttribute.AddOption("15 inch");
			sizeAttribute.AddOption("17 inch");


			sizeAttribute.Save();


			var attributeSet = Context.GetAttributeSetModel("Laptops");
			attributeSet.AddGroup("Monitor");
			attributeSet.AssignAttribute("Monitor", "monitor_sizes");
			attributeSet.Save();
			this.LaptopAttributeSet = attributeSet.Id;
		}

		public long LaptopAttributeSet { get; set; }

		[TearDown]
		public void ProductTeardown()
		{
			DeleteIfExists("TEST-SIMPLEPRODUCT");
			DeleteIfExists("HP-ZBOOK-FURY");
			DeleteIfExists("HP-ZBOOK-FURY-13");
			DeleteIfExists("HP-ZBOOK-FURY-15");
			DeleteIfExists("HP-ZBOOK-FURY-17");


			Context.GetAttributeSetModel("Laptops").Delete();
			Context.GetAttributeModel("monitor_sizes").Delete();
		}

		private void DeleteIfExists(string sku)
		{
			var exists = Context.Products.GetProductBySku(sku) != null;

			if (exists)
			{
				Context.Products.DeleteProduct(sku);
			}
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

			product["test_attr"] = "Xyz";


			product.Save();
		}

		[Test]
		public void CreateConfigurableProduct()
		{
			var product = new ProductModel(this.Context, "HP-ZBOOK-FURY") {
				Name = "HP ZBook Fury",
				AttributeSetId = LaptopAttributeSet,
				Visibility = ProductVisibility.Both,
				Price = 50,
				Type = ProductType.Configurable
			};
			product.Save();

			var smallProduct = new ProductModel(this.Context, "HP-ZBOOK-FURY-13") {
				Price = 2339,
				AttributeSetId = LaptopAttributeSet,
				Visibility = ProductVisibility.NotVisibleIndividually,
				Type = ProductType.Simple,
				["monitor_sizes"] = "13 inch"
			};
			smallProduct.Save();
			var largeProduct = new ProductModel(this.Context, "HP-ZBOOK-FURY-17") {
				Price = 2279,
				AttributeSetId = LaptopAttributeSet,
				Visibility = ProductVisibility.NotVisibleIndividually,
				Type = ProductType.Simple,
				["monitor_sizes"] = "15 inch"
			};

			largeProduct.Save();

			var configurableProduct = product.GetConfigurableProductModel();


			configurableProduct.AddConfigurableOption("monitor_sizes");
			configurableProduct.Save();
			configurableProduct.AddChild(smallProduct);
			configurableProduct.AddChild(largeProduct);
			
			configurableProduct.Save();
		}

		[Test]
		public void GetExistingProduct()
		{
			var product = Context.GetProductModel(SimpleProductSku);

			product.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}