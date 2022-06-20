using FluentAssertions;
using Magento.RestClient.Modules.Customers.Models;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class CustomerTests : AbstractAdminTest
    {

		
     

        [TearDown]
        public void TeardownCustomers()
        {
            var c = MagentoContext.Customers.GetByEmailAddress("customer@example.org");
            MagentoContext.Customers.DeleteById(c.Id);
        }

        [Test]
        public void GetByEmail_WithValidEmail()
        {
            var customer = MagentoContext.Customers.GetByEmailAddress("customer@example.org");


            customer.Email.Should().Be("customer@example.org");
        }
        [Test]
        public void GetByEmail_WithInvalidEmail()
        {
            var customer = MagentoContext.Customers.GetByEmailAddress("doesnotexist@example.org");


            customer.Should().BeNull();
        }

        [Test]
        public void CreateCustomer_CustomerIsValid()
        {
            var shouldBeCreated = new Customer();

            //var c = this.MagentoContext.Customers.Create(shouldBeCreated, "ThisIsThePassword1");
        }
        

        [Test]
        public void CanValidateCustomer()
        {
            var c = this.MagentoContext.Customers.Validate(new Customer());
        }
    }
}