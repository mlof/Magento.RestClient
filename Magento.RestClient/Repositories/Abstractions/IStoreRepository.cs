using System.Collections.Generic;
using Magento.RestClient.Models;

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