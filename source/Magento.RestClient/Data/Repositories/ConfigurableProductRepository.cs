using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Requests;
using Magento.RestClient.Extensions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magento.RestClient.Data.Repositories
{
    internal class ConfigurableProductRepository : AbstractRepository, IConfigurableProductRepository
    {
        public ConfigurableProductRepository(IContext context) : base(context)
        {
            this.RelativeExpiration = TimeSpan.FromMinutes(5);
        }

        public TimeSpan RelativeExpiration { get; set; }

        public Task CreateChild(string parentSku, string childSku)
        {
            var request = new RestRequest("configurable-products/{sku}/child", Method.Post);
            request.SetScope("default");

            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            request.AddJsonBody(new { childSku });

            return ExecuteAsync(request);
        }

        public Task DeleteChild(string parentSku, string childSku)
        {


            var request = new RestRequest("configurable-products/{sku}/children/{childSku}", Method.Delete);
            request.SetScope("default");
            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            request.AddOrUpdateParameter("childSku", childSku, ParameterType.UrlSegment);

            return ExecuteAsync(request);
        }

        public Task<List<Product>> GetConfigurableChildren(string parentSku)
        {
            var request = new RestRequest("configurable-products/{sku}/children", Method.Get);

            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            return ExecuteAsync<List<Product>>(request);
        }

        public Task CreateOption(string parentSku, ConfigurableProductOption option)
        {
            var request = new RestRequest("configurable-products/{sku}/options", Method.Post);


            request.AddJsonBody(new { option = option });
            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            var key = this.Client.BuildUri(request);
            this.Cache.Remove(key);
            return this.Client.ExecuteAsync(request);
        }

        public async Task<List<ConfigurableProductOption>> GetOptions(string parentSku)
        {
            var request = new RestRequest("configurable-products/{sku}/options/all", Method.Get);

            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);

            return await ExecuteAsync<List<ConfigurableProductOption>>(request);


        }

        public Task UpdateOption(string parentSku, long optionId, ConfigurableProductOption option)
        {
            var request = new RestRequest("configurable-products/{sku}/options/{optionId}", Method.Put);
            request.AddJsonBody(new { option = option });
            request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
            request.AddOrUpdateParameter("optionId", optionId, ParameterType.UrlSegment);

            var key = this.Client.BuildUri(request);
            this.Cache.Remove(key);

            return this.Client.ExecuteAsync(request);
        }
        public Task<BulkActionResponse> BulkMergeConfigurations(
            params CreateOrUpdateConfigurationRequest[] configurations)
        {
            var request = new RestRequest("configurable-products/bySku/child", Method.Post);
            request.SetScope("all/async/bulk");


            request.AddJsonBody(configurations.ToList());

            return ExecuteAsync<BulkActionResponse>(request);
        }

        public Task<BulkActionResponse> BulkMergeConfigurableOptions(
            params ConfigurableProductOptionRequest[] requests)
        {
            var request = new RestRequest("configurable-products/bySku/options", Method.Post);
            request.SetScope("all/async/bulk");


            request.AddJsonBody(requests.ToList());

            return ExecuteAsync<BulkActionResponse>(request);
        }


    }
}