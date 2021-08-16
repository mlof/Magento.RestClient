using System;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Category;
using Magento.RestClient.Models.Customers;
using Magento.RestClient.Models.Orders;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Models.Search;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Search.Abstractions
{
    public interface ISearchService
    {
        SearchResponse<AttributeSet> AttributeSets(Action<SearchBuilder<AttributeSet>> configure = null);
        SearchResponse<Customer> Customers(Action<SearchBuilder<Customer>> configure = null);
        SearchResponse<Order> Orders(Action<SearchBuilder<Order>> configure = null);
        SearchResponse<Product> Products(Action<SearchBuilder<Product>> configure = null);
        SearchResponse<AttributeGroup> ProductAttributeGroups(Action<SearchBuilder<AttributeGroup>> configure = null);
        SearchResponse<Category> Categories(Action<SearchBuilder<Category>> configure = null);
		SearchResponse<EntityAttribute> ProductAttributes(Action<SearchBuilder<EntityAttribute>> configure = null);
		SearchResponse<CmsBlock> CmsBlocks(Action<SearchBuilder<CmsBlock>> configure = null);
		SearchResponse<Coupon> Coupons(Action<SearchBuilder<Coupon>> configure = null);
		SearchResponse<CustomerGroup> CustomerGroups(Action<SearchBuilder<CustomerGroup>> configure = null);
	}
}