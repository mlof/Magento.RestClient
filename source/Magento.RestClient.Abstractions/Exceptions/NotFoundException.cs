using System;

namespace Magento.RestClient.Abstractions.Repositories
{
	public class NotFoundException : Exception
	{
		public NotFoundException() : base()
		{
		}

		public NotFoundException(string message) : base(message)
		{
		}

		public NotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}