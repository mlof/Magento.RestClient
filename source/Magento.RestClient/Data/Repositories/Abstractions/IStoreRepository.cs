using System.Collections.Generic;
using Magento.RestClient.Data.Models.Store;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IStoreRepository
	{
		List<Website> GetWebsites();
		List<StoreView> GetStoreViews();
		List<StoreGroup> GetStoreGroups();
		List<StoreConfig> GetStoreConfigs();
	}
}