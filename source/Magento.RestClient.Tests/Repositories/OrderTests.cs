using System.Linq;
using FluentAssertions;
using FluentValidation;
using Magento.RestClient.Modules.Order.Models;
using Magento.RestClient.Search;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class OrderTests : AbstractAdminTest
    {
	    [OneTimeSetUp]

	    public void SetupOrders()
	    {

			
	    }


        [Test]
        public void SearchOrders_WithDefaultSettings()
        {
            var orderResponse = MagentoContext.Orders.AsQueryable().ToList();


        }

    

        [Test]
        public void CreateOrder_InvalidOrder()
        {
            var invalidOrder = new Order() { };


            Assert.Throws<ValidationException>(() => {
                MagentoContext.Orders.CreateOrder(invalidOrder);
            });
        }
    }
}