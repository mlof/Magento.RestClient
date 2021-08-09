using System.Collections.Generic;
using MagentoApi.Models;
using MagentoApi.Repositories.Abstractions;
using RestSharp;

namespace MagentoApi.Repositories
{
    public class ConfigurableProductRepository : IConfigurableProductRepository
    {
        public ConfigurableProductRepository(IRestClient client)
        {
            
        }

        public void CreateChild(string parentSku, string childSku)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteChild(string parentSku, string childSku)
        {
            throw new System.NotImplementedException();
        }

        public List<ConfigurableProduct> GetConfigurableChildren(string parentSku)
        {
            throw new System.NotImplementedException();
        }
    }
}