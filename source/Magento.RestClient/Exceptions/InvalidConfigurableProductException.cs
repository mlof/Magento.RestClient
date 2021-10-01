using System;

namespace Magento.RestClient.Exceptions
{
	public class InvalidConfigurableProductException : Exception
	{
		public InvalidConfigurableProductException(string message) : base(message)
		{
		}

		public InvalidConfigurableProductException() : base()
		{
		}

		public InvalidConfigurableProductException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}