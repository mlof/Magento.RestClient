using System.Collections.Generic;
using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IReadProductRepository
    {
        List<SearchResponse<Product>> Search();
        Product GetProductBySku(string sku);
    }
}