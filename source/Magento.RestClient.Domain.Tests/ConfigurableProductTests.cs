using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Domain.Models.Catalog;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class ConfigurableProductTests : AbstractAdminTest
	{
		[TearDown]
		async public Task ConfigurableProductTearDown()
		{
			DeleteProductIfExists("TEST-CONF-PARENT");
			DeleteProductIfExists("TEST-CONF-CHILD1");
			DeleteProductIfExists("TEST-CONF-CHILD2");
		}

		/// <summary>
		/// CreateConfigurableProduct
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Domain.Exceptions.ConfigurableProductInvalidException">Ignore.</exception>
		/// <exception cref="Magento.RestClient.Domain.Exceptions.ConfigurableChildInvalidException">Ignore.</exception>
		[Test]
		async public Task CreateConfigurableProduct()
		{
			var product = new ConfigurableProductModel(this.Context, "TEST-CONF-PARENT") {
				Name = "TEST CONF PARENT",
				AttributeSetId = this.AttributeSetId,
				Visibility = ProductVisibility.Both,
				Type = ProductType.Configurable
			};
			await product.AddConfigurableOptions("test_configurable_axis");


			var child1 = new ProductModel(Context, "TEST-CONF-CHILD1") {
				Name = "TEST CONF CHILD 1", Price = 45, ["test_configurable_axis"] = "X"
			};
			var child2 = new ProductModel(Context, "TEST-CONF-CHILD2") {
				Name = "TEST CONF CHILD 2", Price = 65, ["test_configurable_axis"] = "Y"
			};

			product.AddChild(child1);
			product.AddChild(child2);
			await product.SaveAsync();


			var children = await Context.ConfigurableProducts.GetConfigurableChildren(product.Sku);
			children.Select(product1 => product1.Sku).Should()
				.BeEquivalentTo(new List<string>() {child1.Sku, child2.Sku});
		}

		/// <summary>
		/// CreateConfigurableProduct_AttachAlreadyAttachedProduct
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Domain.Exceptions.ConfigurableProductInvalidException">Ignore.</exception>
		/// <exception cref="Magento.RestClient.Domain.Exceptions.ConfigurableChildInvalidException">Ignore.</exception>
		[Test]
		async public Task CreateConfigurableProduct_AttachAlreadyAttachedProduct()
		{
			await CreateConfigurableProduct();
			var product = new ConfigurableProductModel(this.Context, "TEST-CONF-PARENT");
			var child1 = new ProductModel(Context, "TEST-CONF-CHILD1") {
				Name = "TEST CONF CHILD 1", Price = 45, ["test_configurable_axis"] = "X"
			};
			product.AddChild(child1);
			await product.SaveAsync();

			var children = await Context.ConfigurableProducts.GetConfigurableChildren(product.Sku);
			children.Select(product1 => product1.Sku).Should()
				.BeEquivalentTo(new List<string>() {"TEST-CONF-CHILD1", "TEST-CONF-CHILD2"});
		}
	}
}