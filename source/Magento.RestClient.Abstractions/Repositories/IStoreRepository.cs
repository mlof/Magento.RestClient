using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Store;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IStoreRepository
	{
		Task<List<Website>> GetWebsites();
		Task<List<StoreView>> GetStoreViews();
		Task<List<StoreGroup>> GetStoreGroups();
		Task<List<StoreConfig>> GetStoreConfigs();
	}
}