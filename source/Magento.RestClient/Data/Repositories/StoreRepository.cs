using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Store;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class StoreRepository : AbstractRepository, IStoreRepository
	{
		public StoreRepository(IContext context) : base(context)
		{
		}

		public Task<List<Website>> GetWebsites()
		{
			var request = new RestRequest("store/websites");
			return ExecuteAsync<List<Website>>(request);
		}

		public Task<List<StoreView>> GetStoreViews()
		{
			var request = new RestRequest("store/storeViews");
			return ExecuteAsync<List<StoreView>>(request);
		}

		public Task<List<StoreGroup>> GetStoreGroups()
		{
			var request = new RestRequest("store/storeGroups");
			return ExecuteAsync<List<StoreGroup>>(request);
		}

		public Task<List<StoreConfig>> GetStoreConfigs()
		{
			var request = new RestRequest("store/storeConfigs");
			return ExecuteAsync<List<StoreConfig>>(request);
		}
	}
}