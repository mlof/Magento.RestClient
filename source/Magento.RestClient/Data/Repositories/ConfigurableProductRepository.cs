using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ConfigurableProductRepository : AbstractRepository, IConfigurableProductRepository
	{
		public ConfigurableProductRepository(IContext context) : base(context)
		{
		}

		public Task CreateChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child", Method.POST);
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddJsonBody(new {childSku});

			return ExecuteAsync(request);
		}

		public Task DeleteChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child/{childSku}", Method.DELETE);
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("childSku", childSku);

			return ExecuteAsync(request);
		}

		public Task<List<Product>> GetConfigurableChildren(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/children", Method.GET);

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			return ExecuteAsync<List<Product>>(request);
		}

		public Task CreateOption(string parentSku, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options", Method.POST);
			request.AddJsonBody(new {option = option});
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			return this.Client.ExecuteAsync(request);
		}

		public Task<List<ConfigurableProductOption>> GetOptions(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/options/all", Method.GET);

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			return ExecuteAsync<List<ConfigurableProductOption>>(request);
		}

		public Task UpdateOption(string parentSku, long optionId, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options/{optionId}", Method.PUT);
			request.AddJsonBody(new {option = option});
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("optionId", optionId, ParameterType.UrlSegment);
			return this.Client.ExecuteAsync(request);
		}
	}
}