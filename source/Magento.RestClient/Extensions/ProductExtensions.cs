using System;
using System.Net;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Models.Stock;
using RestSharp;

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