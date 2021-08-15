using FluentAssertions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class CustomerTests : AbstractIntegrationTest
    {
        public static Customer ShouldExist => new Customer() {
            Email = "customer@example.org", Firstname = "Example", Lastname = "Customer"
        };

        [SetUp]
        public void SetupCustomers()
        {
            Client.Customers.Create(ShouldExist, "ThisIsTheCustomerPassword1%");
        }

        [TearDown]
        public void TeardownCustomers()
        {
            var c = Client.Customers.GetByEmailAddress("customer@example.org");
            Client.Customers.DeleteById(c.Id);
        }

        [Test]
        public void GetByEmail_WithValidEmail()
        {
            var customer = Client.Customers.GetByEmailAddress("customer@example.org");


            customer.Email.Should().Be("customer@example.org");
        }
        [Test]
        public void GetByEmail_WithInvalidEmail()
        {
            var customer = Client.Customers.GetByEmailAddress("doesnotexist@example.org");


            customer.Should().BeNull();
        }

        [Test]
        public void CreateCustomer_CustomerIsValid()
        {
            var shouldBeCreated = new Customer();

            //var c = this.Client.Customers.Create(shouldBeCreated, "ThisIsThePassword1");
        }
        

        [Test]
        public void CanValidateCustomer()
        {
            var c = this.Client.Customers.Validate(new Customer());
        }
    }
}