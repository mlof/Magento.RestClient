using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using NUnit.Framework;
using RestSharp;

namespace Magento.RestClient.Tests.Integration
{
    public class OrderTests : AbstractIntegrationTest
    {
        [Test]
        public void SearchOrders_WithDefaultSettings()
        {
            var orderResponse = Client.Orders.Search();


            orderResponse.SearchCriteria.CurrentPage.Should().Be(SearchBuilder<Order>.DefaultPage);
            orderResponse.SearchCriteria.PageSize.Should().Be(SearchBuilder<Order>.DefaultPageSize);
            orderResponse.SearchCriteria.SortOrders.Should().BeNullOrEmpty();
            orderResponse.SearchCriteria.FilterGroups.Should().BeNullOrEmpty();
            orderResponse.TotalCount.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void SearchOrders_WhereOrderCurrencyCode_EqualsEuro()
        {
            var orderResponse = Client.Orders.Search(builder =>
                builder.Where(order => order.OrderCurrencyCode, SearchCondition.Equals, "EUR"));

            orderResponse.SearchCriteria.FilterGroups.Should().HaveCount(1);
            orderResponse.SearchCriteria.FilterGroups.Single().Filters.Should()
                .ContainEquivalentOf(new Filter() {Value = "EUR", ConditionType = "eq", Field = "order_currency_code"});
        }

        [Test]
        public void CreateOrder_InvalidOrder()
        {
            var invalidOrder = new Order() { };


            Assert.Throws<ValidationException>(() => {
                Client.Orders.CreateOrder(invalidOrder);
            });
        }
    }
}