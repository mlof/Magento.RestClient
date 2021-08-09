using System.Runtime.InteropServices;
using MagentoApi.Models;
using NUnit.Framework;

namespace MagentoApi.Tests.Integration
{
    public class CustomerTests : AbstractIntegrationTest
    {
        private Customer shouldBeCreated = new Customer()
        {
            Id = 30000, Firstname = "SHOULD", Lastname = "BE CREATED"


        };
        private Customer shouldExist = new Customer()
        {
            Id = 50000,
            Firstname = "SHOULD",
            Lastname = "EXIST"


        };

        [SetUp]
        public void SetupCustomers()
        {
            
            client.Customers.DeleteById(shouldBeCreated.Id);
        }
        
        [Test]
        public void CanCreateCustomer()
        {
            var c = this.client.Customers.Create(shouldBeCreated, "ThisIsThePassword1");
        }


        [Test]
        public void CanValidateCustomer()
        {
            var c = this.client.Customers.Validate(new Customer());
            
        }

        [Test]
        public void CanGetBillingAddressForCustomerId()
        {
            this.client.Customers.GetBillingAddress(shouldExist.Id);

        }
        [Test]
        public void CanGetShippingAddressForCustomerId()
        {
            this.client.Customers.GetShippingAddress(shouldExist.Id);

        }

    }
}