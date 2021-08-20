using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions.Customers;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IReadProductRepository 
    {
        Product GetProductBySku(string sku, string scope = "all");
    }
}