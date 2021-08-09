using Magento.RestClient.Models;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class CustomerTests : AbstractIntegrationTest
    {
        private readonly Customer _shouldBeCreated = new()
        {
            Id = 30000, Firstname = "SHOULD", Lastname = "BE CREATED"


        };
        private readonly Customer _shouldExist = new()
        {
            Id = 50000,
            Firstname = "SHOULD",
            Lastname = "EXIST"


        };

        [SetUp]
        public void SetupCustomers()
        {
            
            Client.Customers.DeleteById(_shouldBeCreated.Id);
        }
        
        [Test]
        public void CanCreateCustomer()
        {
            var c = this.Client.Customers.Create(_shouldBeCreated, "ThisIsThePassword1");
        }


        [Test]
        public void CanValidateCustomer()
        {
            var c = this.Client.Customers.Validate(new Customer());
            
        }

        [Test]
        public void CanGetBillingAddressForCustomerId()
        {
            this.Client.Customers.GetBillingAddress(_shouldExist.Id);

        }
        [Test]
        public void CanGetShippingAddressForCustomerId()
        {
            this.Client.Customers.GetShippingAddress(_shouldExist.Id);

        }

    }
}