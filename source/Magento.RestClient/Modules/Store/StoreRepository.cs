using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Store.Abstractions;
using Magento.RestClient.Modules.Store.Models;
using RestSharp;

namespace Magento.RestClient.Modules.Store
{
	public class StoreRepository : AbstractRepository, IStoreRepository
	{
		public StoreRepository(IMagentoContext magentoContext) : base(magentoContext)
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