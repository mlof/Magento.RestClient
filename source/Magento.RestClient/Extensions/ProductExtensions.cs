using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Models.Stock;

namespace Magento.RestClient.Extensions
{
	public static class ProductExtensions
	{
		public static void SetStockItem(this Product product, StockItem stockItem)
		{
			product.ExtensionAttributes.stock_item = stockItem;
		}
	}
}