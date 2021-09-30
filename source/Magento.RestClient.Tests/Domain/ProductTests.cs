﻿using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Tests.Domain.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	class ProductTests : AbstractDomainObjectTest
	{
		[SetUp]
		async public Task ProductSetup()
		{
			var sizeAttribute = Context.GetAttributeModel("monitor_sizes");
			sizeAttribute.DefaultFrontendLabel = "Monitor Size";
			sizeAttribute.FrontendInput = AttributeFrontendInput.Select;
			sizeAttribute.AddOption("13 inch");
			sizeAttribute.AddOption("14 inch");
			sizeAttribute.AddOption("15 inch");
			sizeAttribute.AddOption("17 inch");


			await sizeAttribute.SaveAsync();


			var attributeSet = Context.GetAttributeSetModel("Laptops");
			attributeSet.AddGroup("Monitor");
			attributeSet.AssignAttribute("Monitor", "monitor_sizes");
			await attributeSet.SaveAsync();
			this.LaptopAttributeSet = attributeSet.Id;
		}

		public long LaptopAttributeSet { get; set; }

		[TearDown]
		async public Task ProductTeardown()
		{
			DeleteIfExists("TEST-SIMPLEPRODUCT");
			DeleteIfExists("HP-ZBOOK-FURY");
			DeleteIfExists("HP-ZBOOK-FURY-13");
			DeleteIfExists("HP-ZBOOK-FURY-15");
			DeleteIfExists("HP-ZBOOK-FURY-17");


			await Context.GetAttributeSetModel("Laptops").Delete();
			await Context.GetAttributeModel("monitor_sizes").Delete();
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
		async public Task CreateSimpleProduct()
		{
			var sku = "TEST-SIMPLEPRODUCT";
			var product = new ProductModel(this.Context, sku);

			product.Name = "TestProduct";
			product.AttributeSetId = 28;

			product.Visibility = ProductVisibility.Both;
			product.Price = 50;

			product["test_attr"] = "Xyz";


			await product.SaveAsync();
		}

		[Test]
		async public Task CreateConfigurableProduct()
		{
			var product = new ConfigurableProductModel(this.Context, "HP-ZBOOK-FURY") {
				Name = "HP ZBook Fury",
				AttributeSetId = this.LaptopAttributeSet,
				Visibility = ProductVisibility.Both,
				Price = 50,
				Type = ProductType.Configurable
			};

			var smallProduct = new ProductModel(this.Context, "HP-ZBOOK-FURY-13") {
				Price = 2339,
				AttributeSetId = this.LaptopAttributeSet,
				Visibility = ProductVisibility.NotVisibleIndividually,
				Type = ProductType.Simple,
				["monitor_sizes"] = "13 inch"
			};
			product.AddChild(smallProduct);
			var largeProduct = new ProductModel(this.Context, "HP-ZBOOK-FURY-17") {
				Price = 2279,
				AttributeSetId = this.LaptopAttributeSet,
				Visibility = ProductVisibility.NotVisibleIndividually,
				Type = ProductType.Simple,
				["monitor_sizes"] = "15 inch"
			};

			product.AddChild(largeProduct);



			await product.AddConfigurableOptions("monitor_sizes");

			await product.SaveAsync();
		}

		[Test]
		public void GetExistingProduct()
		{
			var product = Context.GetProductModel(SimpleProductSku);

			product.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}