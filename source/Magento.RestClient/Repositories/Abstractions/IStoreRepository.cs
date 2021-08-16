using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Store;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IStoreRepository
    {
        List<Website> GetWebsites();
        List<StoreView> GetStoreViews();
        List<StoreGroup> GetStoreGroups();
        List<StoreConfig> GetStoreConfigs();
    }
}