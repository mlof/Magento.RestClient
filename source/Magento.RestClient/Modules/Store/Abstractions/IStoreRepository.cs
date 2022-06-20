using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Modules.Store.Models;

namespace Magento.RestClient.Modules.Store.Abstractions
{
	public interface IStoreRepository
	{
		Task<List<Website>> GetWebsites();
		Task<List<StoreView>> GetStoreViews();
		Task<List<StoreGroup>> GetStoreGroups();
		Task<List<StoreConfig>> GetStoreConfigs();
	}
}