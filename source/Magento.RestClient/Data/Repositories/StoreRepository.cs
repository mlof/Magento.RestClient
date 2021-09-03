using System.Collections.Generic;
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

		public List<Website> GetWebsites()
		{
			var request = new RestRequest("store/websites");
			var response = _client.Execute<List<Website>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public List<StoreView> GetStoreViews()
		{
			var request = new RestRequest("store/storeViews");
			var response = _client.Execute<List<StoreView>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public List<StoreGroup> GetStoreGroups()
		{
			var request = new RestRequest("store/storeGroups");
			var response = _client.Execute<List<StoreGroup>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public List<StoreConfig> GetStoreConfigs()
		{
			var request = new RestRequest("store/storeConfigs");
			var response = _client.Execute<List<StoreConfig>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}
	}
}