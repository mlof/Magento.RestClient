using System;

namespace Magento.RestClient.Domain.Models
{
	public class ProductSkuInvalidException : Exception
	{
		public ProductSkuInvalidException(string sku) : base($"{sku} contains illegal characters.")
		{
		}
	}
}