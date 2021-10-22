using System;

namespace Magento.RestClient.Domain.Models.Catalog.Exceptions
{
	public class ProductSkuInvalidException : Exception
	{
		public ProductSkuInvalidException(string sku) : base($"{sku} contains illegal characters.")
		{
		}

		public ProductSkuInvalidException() : base()
		{
		}

		public ProductSkuInvalidException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}