using System;

namespace Magento.RestClient.Exceptions.Generic
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public BadRequestException() : base()
		{
		}

		public BadRequestException(string message) : base(message)
		{
		}
	}
}