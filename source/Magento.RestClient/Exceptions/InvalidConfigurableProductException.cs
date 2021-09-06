using System;

namespace Magento.RestClient.Exceptions
{
	public class InvalidConfigurableProductException : Exception
	{
		public InvalidConfigurableProductException(string message) : base(message)
		{
		}
	}
}