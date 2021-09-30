﻿using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Models.Stock;
using Newtonsoft.Json;

namespace Magento.RestClient.Extensions
{
	public static class ProductExtensions
	{
		public static void SetStockItem(this Product product, StockItem stockItem)
		{
			product.ExtensionAttributes.stock_item = stockItem;
		}

		public static StockItem GetStockItem(this Product product)
		{
			return product.ExtensionAttributes.stock_item;
		}
	}
}