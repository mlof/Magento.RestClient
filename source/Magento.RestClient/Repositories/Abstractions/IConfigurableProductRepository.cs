using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Products;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IConfigurableProductRepository
    {
        public void CreateChild(string parentSku, string childSku);
        public void DeleteChild (string parentSku, string childSku);
        public List<ConfigurableProduct> GetConfigurableChildren(string parentSku);


        void CreateOption(string parentSku, long attributeId, int valueId, string label);
    }
}