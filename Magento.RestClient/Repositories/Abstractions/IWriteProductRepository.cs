using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IWriteProductRepository
    {
        Product CreateProduct(Product product, bool saveOptions= true);
        Product UpdateProduct (string sku, Product product, bool saveOptions = true);
        void DeleteProduct  (string sku);
    }
}