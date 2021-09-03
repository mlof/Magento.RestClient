using FluentAssertions;
using Magento.RestClient.Models.Customers;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class CustomerTests : AbstractIntegrationTest
    {
        public static Customer ShouldExist => new Customer() {
            Email = "customer@example.org", Firstname = "Example", Lastname = "Customer"
        };

        [SetUp]
        public void SetupCustomers()
        {
            Context.Customers.Create(ShouldExist, "ThisIsTheCustomerPassword1%");
        }

        [TearDown]
        public void TeardownCustomers()
        {
            var c = Context.Customers.GetByEmailAddress("customer@example.org");
            Context.Customers.DeleteById(c.Id);
        }

        [Test]
        public void GetByEmail_WithValidEmail()
        {
            var customer = Context.Customers.GetByEmailAddress("customer@example.org");


            customer.Email.Should().Be("customer@example.org");
        }
        [Test]
        public void GetByEmail_WithInvalidEmail()
        {
            var customer = Context.Customers.GetByEmailAddress("doesnotexist@example.org");


            customer.Should().BeNull();
        }

        [Test]
        public void CreateCustomer_CustomerIsValid()
        {
            var shouldBeCreated = new Customer();

            //var c = this.Context.Customers.Create(shouldBeCreated, "ThisIsThePassword1");
        }
        

        [Test]
        public void CanValidateCustomer()
        {
            var c = this.Context.Customers.Validate(new Customer());
        }
    }
}