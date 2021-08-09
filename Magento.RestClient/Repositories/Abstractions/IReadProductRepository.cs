using System.Collections.Generic;
using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IReadProductRepository
    {
        List<SearchResponse<Product>> Search();
        Product GetProductBySku(string sku);
    }
}