using System;

namespace Magento.RestClient.Exceptions.ConfigurableProduct
{
	public class ConfigurableProductInvalidException : Exception
	{
		public ConfigurableProductInvalidException(string message) : base(message)
		{
		}

		public ConfigurableProductInvalidException() : base()
		{
		}

		public ConfigurableProductInvalidException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}