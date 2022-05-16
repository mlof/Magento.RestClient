using System;

namespace Magento.RestClient.Exceptions.Authentication
{
	public class InvalidAuthenticationMethodException : Exception
	{
		public InvalidAuthenticationMethodException()
		{
		}

		public InvalidAuthenticationMethodException(string message) : base(message)
		{
		}

		public InvalidAuthenticationMethodException(string message, Exception innerException) : base(message,
			innerException)
		{
		}
	}
}