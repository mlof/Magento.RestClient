using System.Collections.Generic;
using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IConfigurableProductRepository
    {
        public void CreateChild(string parentSku, string childSku);
        public void DeleteChild (string parentSku, string childSku);
        public List<ConfigurableProduct> GetConfigurableChildren(string parentSku);


    }
}