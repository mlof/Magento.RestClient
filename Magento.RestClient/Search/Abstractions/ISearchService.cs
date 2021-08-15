using System;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Search.Abstractions
{
    public interface ISearchService
    {
        SearchResponse<AttributeSet> AttributeSets(Action<SearchBuilder<AttributeSet>> configure = null);
        SearchResponse<Customer> Customers(Action<SearchBuilder<Customer>> configure = null);
        SearchResponse<Order> Orders(Action<SearchBuilder<Order>> configure = null);
        SearchResponse<Product> Products(Action<SearchBuilder<Product>> configure = null);
    }
}