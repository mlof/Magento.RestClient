using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Store;
using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class StoreRepository : AbstractRepository, IStoreRepository
	{

		public StoreRepository(IContext context) : base(context)
		{
		}

		public async Task<List<Website>> GetWebsites()
		{
			var request = new RestRequest("store/websites");
			var response = await Client.ExecuteAsync<List<Website>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public async Task<List<StoreView>> GetStoreViews()
		{
			var request = new RestRequest("store/storeViews");
			var response = await Client.ExecuteAsync<List<StoreView>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public async Task<List<StoreGroup>> GetStoreGroups()
		{
			var request = new RestRequest("store/storeGroups");
			var response = await Client.ExecuteAsync<List<StoreGroup>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}

		public async Task<List<StoreConfig>> GetStoreConfigs()
		{
			var request = new RestRequest("store/storeConfigs");
			var response = await Client.ExecuteAsync<List<StoreConfig>>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw response.ErrorException;
		}
	}
}