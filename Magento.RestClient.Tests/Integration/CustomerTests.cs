using Magento.RestClient.Models;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class CustomerTests : AbstractIntegrationTest
    {
        [SetUp]
        public void SetupCustomers()
        {
        }

        [Test]
        public void CreateCustomer_CustomerIsValid()
        {
            var shouldBeCreated = new Customer();

            var c = this.Client.Customers.Create(shouldBeCreated, "ThisIsThePassword1");
        }

        [Test]
        public void CreateCustomer_CustomerIsNotValid()
        {
            var shouldNotBeCreated = new Customer();

            var c = this.Client.Customers.Create(shouldNotBeCreated, "ThisIsThePassword1");

        }

        [Test]
        public void CanValidateCustomer()
        {
            var c = this.Client.Customers.Validate(new Customer());
        }


    }
}