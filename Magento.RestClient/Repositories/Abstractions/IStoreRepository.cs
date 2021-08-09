using System.Collections.Generic;
using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IStoreRepository
    {
        List<Website> GetWebsites();
        List<StoreView> GetStoreViews();
        List<StoreGroup> GetStoreGroups();
        List<StoreConfig> GetStoreConfigs();
    }
}