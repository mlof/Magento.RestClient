using System.Collections.Generic;
using System.Threading.Tasks;
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
		private readonly IRestClient client;

		public ConfigurableProductRepository(IRestClient client)
		{
			this.client = client;
		}

		async public Task CreateChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddJsonBody(new {childSku});

			var response = await client.ExecuteAsync(request);
			HandleResponse(response);
		}

		async public Task DeleteChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child/{childSku}");
			request.Method = Method.DELETE;
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("childSku", childSku);

			var response = await client.ExecuteAsync(request);
			if (!response.IsSuccessful)
			{
				throw MagentoException.Parse(response.Content);
			}
		}

		async public Task<List<Product>> GetConfigurableChildren(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/children");
			request.Method = Method.GET;

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);

			var response = await client.ExecuteAsync<List<Product>>(request);
			return HandleResponse(response) ?? new List<Product>();
		}


		async public Task CreateOption(string parentSku, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options");
			request.Method = Method.POST;
			request.AddJsonBody(new {option = option});
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			await client.ExecuteAsync(request);
		}

		async public Task<List<ConfigurableProductOption>> GetOptions(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/options/all");
			request.Method = Method.GET;

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			var response = await client.ExecuteAsync<List<ConfigurableProductOption>>(request);
			return HandleResponse(response);
		}

		public async Task UpdateOption(string parentSku, long optionId, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options/{optionId}");
			request.Method = Method.PUT;
			request.AddJsonBody(new {option = option});
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("optionId", optionId, ParameterType.UrlSegment);
			await client.ExecuteAsync(request);
		}
	}
}