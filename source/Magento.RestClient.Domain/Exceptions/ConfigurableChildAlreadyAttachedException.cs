using System;

namespace Magento.RestClient.Domain.Exceptions
{
	public class ConfigurableChildAlreadyAttachedException : Exception
	{
		public ConfigurableChildAlreadyAttachedException() : base()
		{
		}

		public ConfigurableChildAlreadyAttachedException(string message) : base(message)
		{
		}

		public ConfigurableChildAlreadyAttachedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}