using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Magento.RestClient.Domain;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories;
using NUnit.Framework;
using Order = Magento.RestClient.Models.Order;

namespace Magento.RestClient.Tests.Integration
{
    public class CartTests : AbstractIntegrationTest
    {
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
        }


        [Test]
        public void CreateCart()
        {
            var cart = Cart.CreateNew(Client.Carts);
            cart.Id.Should().NotBe(0);
        }

        [Test]
        public void GetExistingCart()
        {
            var cart = Cart.GetExisting(Client.Carts, 8);


            cart.Id.Should().NotBe(0);
        }

        [Test]
        public void Cart_AssignCustomer_ValidCustomer()
        {
            var cart = GetCart();


            cart.AssignCustomer(1);

            cart.Customer.Should().NotBeNull();
            cart.Customer.Email.Should().NotBeNullOrEmpty();
            cart.Customer.Firstname.Should().NotBeNullOrEmpty();
            cart.Customer.Lastname.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Cart_AssignCustomer_InvalidCustomer()
        {
            var cart = GetCart();


            Assert.Throws<EntityNotFoundException>(
                () => {
                    cart.AssignCustomer(-1);

                    cart.Customer.Should().BeNull();
                }
            );
        }

        private Cart GetCart()
        {
            var cart = Cart.CreateNew(Client.Carts);
            cart.ShippingAddress = ScunthorpePostOffice;
            cart.BillingAddress = ScunthorpePostOffice;
            return cart;
        }


        [Test]
        public void Cart_AddItem_ValidItem()
        {
            var cart = Cart.CreateNew(Client.Carts);
            cart.AddItem("TESTPRODUCT", 3);
            cart.Items.Any(item => item.Sku == "TESTPRODUCT" && item.Qty == 3).Should().BeTrue();
        }

        [Test]
        public void Cart_AddItem_InvalidItem()
        {
            var cart = Cart.CreateNew(Client.Carts);

            Assert.Throws<MagentoException>(() => {
                cart.AddItem("DOESNOTEXIST", 3);
            });
        }

        public void Car()
        {
            var cart = Cart.CreateNew(Client.Carts);


            cart.AddItem("TESTPRODUCT", 3)
                .AddItem("TESTPRODUCT", 3);

            var cheapestShipping = cart.EstimateShippingMethods()
                .OrderByDescending(method => method.PriceInclTax)
                .First();
            var paymentMethod = cart.GetPaymentMethods()
                .First();

            cart.SetPaymentMethod(paymentMethod.Code)
                .SetShippingMethod(cheapestShipping);
            var orderId = cart.Commit();

            var order = Domain.Order.GetExisting(Client.Orders, orderId);

            order.CreateInvoice();
        }
    }
}