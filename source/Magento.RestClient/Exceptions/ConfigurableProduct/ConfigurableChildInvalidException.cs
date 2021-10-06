using System;
using System.Collections.Generic;

namespace Magento.RestClient.Exceptions.ConfigurableProduct
{
	public class ConfigurableChildInvalidException : Exception
	{
		public ConfigurableChildInvalidException() : base()
		{
		}

		public ConfigurableChildInvalidException(string message) : base(message)
		{
		}
		public ConfigurableChildInvalidException(string message, Exception e) : base(message, e)
		{
		}

		public List<string> MissingAttributes { get; set; }
	}
}