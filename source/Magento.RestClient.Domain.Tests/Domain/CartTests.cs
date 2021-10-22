using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models.Cart;
using Magento.RestClient.Domain.Models.Catalog;
using Magento.RestClient.Exceptions.Generic;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class CartTests : AbstractAdminTest
	{
		private long existingCartId;

		public static Address ScunthorpePostOffice => new Address() {
			Firstname = "Scunthorpe",
			Lastname = "Post Office",
			Telephone = "+44 1724 843348",
			Company = "Scunthorpe Post Office",
			City = "Scunthorpe",
			Street = new List<string>() {"148 High St"},
			Postcode = "DN15 6EN",
			CountryId = "GB"
		};

		[SetUp]
		public async Task SetupCart()
		{
			var cart = new CartModel(Context);
			this.existingCartId = cart.Id;


			var product = new ProductModel(Context, this.CartProductSku);

			product.Price = 10;
			product.Name = "CART_PRODUCT";
			product.SetStock(10);
			await product.SaveAsync();
		}

		public string CartProductSku { get; set; } = "TEST_CART_PRODUCT";

		[TearDown]
		public void TearDownCart()
		{
			Context.Products.DeleteProduct("TEST_CART_PRODUCT");
		}

		[Test]
		public void CreateCart()
		{
			var cart = Context.CreateNewCartModel();
			cart.Id.Should().NotBe(0);
		}

		[Test]
		public void GetExistingCart()
		{
			var cart = Context.GetExistingCartModel(existingCartId);

			cart.Id.Should().NotBe(0);
		}

		[Test]
		async public Task Cart_AssignCustomer_ValidCustomer()
		{
			var cart = new CartModel(Context);

			await cart.AssignCustomer(1);

			cart.Customer.Should().NotBeNull();
			cart.Customer.Email.Should().NotBeNullOrEmpty();
			cart.Customer.Firstname.Should().NotBeNullOrEmpty();
			cart.Customer.Lastname.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Cart_AssignCustomer_InvalidCustomer()
		{
			var cart = Context.CreateNewCartModel();


			Assert.ThrowsAsync<EntityNotFoundException>(async () => {
					await cart.AssignCustomer(-1);

					cart.Customer.Should().BeNull();
				}
			);
		}


		[Test]
		async public Task Cart_AddSimpleProduct_ValidItem()
		{
			var cart = new CartModel(Context);
			await cart.AddSimpleProduct(this.CartProductSku, 3);

			cart.Items.Any(item => item.Sku == this.CartProductSku && item.Qty == 3).Should().BeTrue();
		}

		[Test]
		public void Cart_AddSimpleProduct_InvalidItem()
		{
			var cart = new CartModel(Context);

			Assert.ThrowsAsync<MagentoException>(async () => {
				await cart.AddSimpleProduct("DOESNOTEXIST", 3);
			});
		}

		[Test]
		async public Task Cart_AddConfigurableProduct()
		{
			var cart = new CartModel(Context);
			await cart.AddConfigurableProduct("TESTSKU-CONF", "TESTSKU-CONF-option 1-option 2", 3);
		}


		[Test]
		async public Task ShippingMethods_GetMethods_ShippingAddressSet()
		{
			var cart = new CartModel(Context);
			await cart.AddSimpleProduct(this.CartProductSku, 3);

			cart.BillingAddress = ScunthorpePostOffice;
			cart.ShippingAddress = ScunthorpePostOffice;
			var shippingMethods = await cart.EstimateShippingMethods();

			Assert.IsNotEmpty(shippingMethods);
		}

		[Test]
		public void ShippingMethods_GetMethods_ShippingAddressSet_ItemsEmpty()
		{
			var cart = new CartModel(Context);
			cart.BillingAddress = ScunthorpePostOffice;
			cart.ShippingAddress = ScunthorpePostOffice;

			Assert.Throws<ArgumentNullException>(() => {
				var shippingMethods = cart.EstimateShippingMethods();
			});
		}

		[Test]
		async public Task ShippingMethods_GetMethods_ShippingAddressNotSet()
		{
			var cart = new CartModel(Context);

			await cart.AddSimpleProduct(this.CartProductSku, 3);

			Assert.ThrowsAsync<ArgumentNullException>(async () =>
				await cart.EstimateShippingMethods());
		}

		[Test]
		async public Task ShippingMethods_SetShippingMethod_Cheapest()
		{
			var cart = new CartModel(Context);

			await cart.AddSimpleProduct(this.CartProductSku, 3);

			cart.ShippingAddress = ScunthorpePostOffice;
			var shippingMethods = await cart.EstimateShippingMethods();
			await cart.SetShippingMethod(shippingMethods.First());
		}

		/// <summary>
		/// ShippingMethods_SetShippingMethod_InvalidShippingMethod
		/// </summary>
		/// <exception cref="InvalidOperationException">Ignore.</exception>
		[Test]
		async public Task ShippingMethods_SetShippingMethod_InvalidShippingMethod()
		{
			var cart = new CartModel(Context);

			await cart.AddSimpleProduct(this.CartProductSku, 3);

			cart.BillingAddress = ScunthorpePostOffice;
			cart.ShippingAddress = ScunthorpePostOffice;

			Assert.ThrowsAsync<InvalidOperationException>(async () => {
				await cart.SetShippingMethod("Yodel", "THISISNOTAVALIDSHIPPINGMETHOD");
			});
		}

		[Test]
		async public Task PaymentMethods_GetMethods()
		{
			var cart = new CartModel(Context);

			var methods = await cart.GetPaymentMethods();
			methods.Should().NotBeNullOrEmpty();
		}

		/// <summary>
		/// PaymentMethods_SetPaymentMethods_InvalidPaymentMethod
		/// </summary>
		/// <exception cref="InvalidOperationException">Ignore.</exception>
		[Test]
		public async Task PaymentMethods_SetPaymentMethods_InvalidPaymentMethodAsync()
		{
			var cart = new CartModel(Context);


			Assert.ThrowsAsync<InvalidOperationException>(async () => {
				await cart.SetPaymentMethod("GALLONOFPCP");
			});
		}

		[Test]
		async public Task PaymentMethods_SetPaymentMethods_ValidPaymentMethod()
		{
			var cart = new CartModel(Context);

			var paymentMethod = await cart.GetPaymentMethods();
			await cart.SetPaymentMethod(paymentMethod.First().Code);
		}

		/// <summary>
		/// CommitOrder_ValidOrder
		/// </summary>
		/// <exception cref="InvalidOperationException">Ignore.</exception>
		async public Task CommitOrder_ValidOrder()
		{
			var cart = new CartModel(Context);

			cart.ShippingAddress = ScunthorpePostOffice;
			cart.BillingAddress = ScunthorpePostOffice;

			await cart.AddSimpleProduct(this.CartProductSku, 3);

			var cheapestShipping = await cart.EstimateShippingMethods();
			var paymentMethods = await cart.GetPaymentMethods();


			await cart.SetPaymentMethod(paymentMethods.First());
			await cart.SetShippingMethod(cheapestShipping.First());
			var orderId = await cart.Commit();
			orderId.Should().NotBe(0);
		}
	}
}