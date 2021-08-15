using System;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search.Abstractions;
using Magento.RestClient.Search.Extensions;
using RestSharp;

namespace Magento.RestClient.Search
{
    internal class SearchService : ISearchService
    {
        private readonly IRestClient _client;

        public SearchService(IRestClient client)
        {
            this._client = client;
        }

        public SearchResponse<Customer> Customers(Action<SearchBuilder<Customer>> configure = null)
        {
            return Search("customers/search", configure);
        }

        private SearchResponse<T> Search<T>(string resource, Action<SearchBuilder<T>> configure)
        {
            var request = new RestRequest(resource)
                .Search(configure);
            var response = _client.Execute<SearchResponse<T>>(request);
            return response.Data;
        }

        public SearchResponse<AttributeSet> AttributeSets(Action<SearchBuilder<AttributeSet>> configure = null)
        {
            return Search("eav/attribute-sets/list", configure);
        }

        public SearchResponse<Order> Orders(Action<SearchBuilder<Order>> configure = null)
        {
            return Search("orders", configure);
        }

        public SearchResponse<Product> Products(Action<SearchBuilder<Product>> configure = null)
        {
            return Search("products", configure);
        }

        public SearchResponse<AttributeGroup> ProductAttributeGroups(
            Action<SearchBuilder<AttributeGroup>> configure = null)
        {
            return Search("products/attribute-sets/groups/list", configure);
        }

        public SearchResponse<EntityAttribute> ProductAttributes(
            Action<SearchBuilder<EntityAttribute>> configure = null)
        {
            return Search("products/attributes", configure);
        }

        public SearchResponse<Category> Categories(
            Action<SearchBuilder<Category>> configure = null)
        {
            return Search("categories/list", configure);
        }

        public SearchResponse<CmsBlock> CmsBlocks(
            Action<SearchBuilder<CmsBlock>> configure = null)
        {
            return Search("cmsBlock/search", configure);
        }

        public SearchResponse<Coupon> Coupons(
            Action<SearchBuilder<Coupon>> configure = null)
        {
            return Search("coupons/search", configure);
        }

        public SearchResponse<CustomerGroup> CustomerGroups(
            Action<SearchBuilder<CustomerGroup>> configure = null)
        {
            return Search("customerGroups/search", configure);
        }
    }
}