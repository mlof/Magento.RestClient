using System.Linq;
using FluentAssertions;
using FluentValidation;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Data.Models.Search;
using Magento.RestClient.Search;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class OrderTests : AbstractIntegrationTest
    {
        [Test]
        public void SearchOrders_WithDefaultSettings()
        {
            var orderResponse = Context.Orders.AsQueryable().ToList();


        }

    

        [Test]
        public void CreateOrder_InvalidOrder()
        {
            var invalidOrder = new Order() { };


            Assert.Throws<ValidationException>(() => {
                Context.Orders.CreateOrder(invalidOrder);
            });
        }
    }
}