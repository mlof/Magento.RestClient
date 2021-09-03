using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using Magento.RestClient.Exceptions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class CartTests : AbstractDomainObjectTest
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
		public void SetupCart()
		{
			var cart = new CartModel(Context);
			this.existingCartId = cart.Id;
		}

		[TearDown]
		public void TearDownCart()
		{
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
		public void Cart_AssignCustomer_ValidCustomer()
		{
			var cart = new CartModel(Context);

			cart.AssignCustomer(1);

			cart.Customer.Should().NotBeNull();
			cart.Customer.Email.Should().NotBeNullOrEmpty();
			cart.Customer.Firstname.Should().NotBeNullOrEmpty();
			cart.Customer.Lastname.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Cart_AssignCustomer_InvalidCustomer()
		{
			var cart = Context.CreateNewCartModel();


			Assert.Throws<EntityNotFoundException>(
				() => {
					cart.AssignCustomer(-1);

					cart.Customer.Should().BeNull();
				}
			);
		}


		[Test]
		public void Cart_AddSimpleProduct_ValidItem()
		{
			var cart = new CartModel(Context);
			cart.AddSimpleProduct(SimpleProductSku, 3);

			cart.Items.Any(item => item.Sku == SimpleProductSku && item.Qty == 3).Should().BeTrue();
		}

		[Test]
		public void Cart_AddSimpleProduct_InvalidItem()
		{
			var cart = new CartModel(Context);

			Assert.Throws<MagentoException>(() => {
				cart.AddSimpleProduct("DOESNOTEXIST", 3);
			});
		}

		[Test]
		public void Cart_AddConfigurableProduct()
		{
			var cart = new CartModel(Context);
			cart.AddConfigurableProduct("TESTSKU-CONF", "TESTSKU-CONF-option 1-option 2", 3);
		}


		[Test]
		public void ShippingMethods_GetMethods_ShippingAddressSet()
		{
			var cart = new CartModel(Context);
			cart.AddSimpleProduct(SimpleProductSku, 3);

			cart.BillingAddress = ScunthorpePostOffice;
			cart.ShippingAddress = ScunthorpePostOffice;
			var shippingMethods = cart.EstimateShippingMethods();

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
		public void ShippingMethods_GetMethods_ShippingAddressNotSet()
		{
			var cart = new CartModel(Context);

			cart.AddSimpleProduct(SimpleProductSku, 3);

			Assert.Throws<ArgumentNullException>(() =>
				cart.EstimateShippingMethods());
		}

		[Test]
		public void ShippingMethods_SetShippingMethod_Cheapest()
		{
			var cart = new CartModel(Context);

			cart.AddSimpleProduct(SimpleProductSku, 3);

			cart.ShippingAddress = ScunthorpePostOffice;
			var shippingMethods = cart.EstimateShippingMethods();
			var cheapestShipping = cart.EstimateShippingMethods()
				.OrderByDescending(method => method.PriceInclTax)
				.First();

			cart.SetShippingMethod(cheapestShipping);
		}

		/// <summary>
		/// ShippingMethods_SetShippingMethod_InvalidShippingMethod
		/// </summary>
		/// <exception cref="InvalidOperationException">Ignore.</exception>
		[Test]
		public void ShippingMethods_SetShippingMethod_InvalidShippingMethod()
		{
			var cart = new CartModel(Context);

			cart.AddSimpleProduct(SimpleProductSku, 3);

			cart.BillingAddress = ScunthorpePostOffice;
			cart.ShippingAddress = ScunthorpePostOffice;

			Assert.Throws<InvalidOperationException>(() => {
				cart.SetShippingMethod("Yodel", "THISISNOTAVALIDSHIPPINGMETHOD");
			});
		}

		[Test]
		public void PaymentMethods_GetMethods()
		{
			var cart = new CartModel(Context);

			var methods = cart.GetPaymentMethods();
			methods.Should().NotBeNullOrEmpty();
		}

		/// <summary>
		/// PaymentMethods_SetPaymentMethods_InvalidPaymentMethod
		/// </summary>
		/// <exception cref="InvalidOperationException">Ignore.</exception>
		[Test]
		public void PaymentMethods_SetPaymentMethods_InvalidPaymentMethod()
		{
			var cart = new CartModel(Context);


			Assert.Throws<InvalidOperationException>(() => {
				cart.SetPaymentMethod("GALLONOFPCP");
			});
		}

		[Test]
		public void PaymentMethods_SetPaymentMethods_ValidPaymentMethod()
		{
			var cart = new CartModel(Context);

			var paymentMethod = cart.GetPaymentMethods()
				.First();
			cart.SetPaymentMethod(paymentMethod.Code);
		}

		/// <summary>
		/// CommitOrder_ValidOrder
		/// </summary>
		/// <exception cref="InvalidOperationException">Ignore.</exception>
		public void CommitOrder_ValidOrder()
		{
			var cart = new CartModel(Context);

			cart.ShippingAddress = ScunthorpePostOffice;
			cart.BillingAddress = ScunthorpePostOffice;

			cart.AddSimpleProduct(SimpleProductSku, 3);

			var cheapestShipping = cart.EstimateShippingMethods()
				.OrderByDescending(method => method.PriceInclTax)
				.First();
			var paymentMethod = cart.GetPaymentMethods()
				.First();


			cart.SetPaymentMethod(paymentMethod)
				.SetShippingMethod(cheapestShipping);
			var orderId = cart.Commit();
			orderId.Should().NotBe(0);
		}
	}
}