using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Store;
using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class StoreRepository : IStoreRepository
	{
		private readonly IRestClient _client;

		public StoreRepository(IRestClient client)
		{
			_client = client;
		}

		async public Task<List<Website>> GetWebsites()
		{
			var request = new RestRequest("store/websites");
			var response = await _client.ExecuteAsync<List<Website>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		async public Task<List<StoreView>> GetStoreViews()
		{
			var request = new RestRequest("store/storeViews");
			var response = await _client.ExecuteAsync<List<StoreView>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		async public Task<List<StoreGroup>> GetStoreGroups()
		{
			var request = new RestRequest("store/storeGroups");
			var response = await _client.ExecuteAsync<List<StoreGroup>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		async public Task<List<StoreConfig>> GetStoreConfigs()
		{
			var request = new RestRequest("store/storeConfigs");
			var response = await _client.ExecuteAsync<List<StoreConfig>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}
	}
}