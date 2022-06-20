using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Modules.AsynchronousOperations.Models;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;
using Magento.RestClient.Requests;
using RestSharp;

namespace Magento.RestClient.Modules.Catalog
{
	internal class ConfigurableProductRepository : AbstractRepository, IConfigurableProductRepository
	{
		public ConfigurableProductRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
			this.RelativeExpiration = TimeSpan.FromMinutes(5);
		}

		public TimeSpan RelativeExpiration { get; set; }

		public Task CreateChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child", Method.Post);
			request.SetScope("default");

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddJsonBody(new {childSku});

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
			var request = new RestRequest("configurable-products/{sku}/children");

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			return ExecuteAsync<List<Product>>(request);
		}

		public Task CreateOption(string parentSku, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options", Method.Post);


			request.AddJsonBody(new {option});
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			var key = this.Client.BuildUri(request);
			this.Cache.Remove(key);
			return this.Client.ExecuteAsync(request);
		}

		public async  Task<List<ConfigurableProductOption>> GetOptions(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/options/all");

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);

			return await ExecuteAsync<List<ConfigurableProductOption>>(request);
		}

		public Task UpdateOption(string parentSku, long optionId, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options/{optionId}", Method.Put);
			request.AddJsonBody(new {option});
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