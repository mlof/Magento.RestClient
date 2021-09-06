using System;
using System.Collections.Generic;

namespace Magento.RestClient.Exceptions
{
	public class InvalidChildProductException : Exception
	{
		public InvalidChildProductException(string message) : base(message)
		{
		}

		public List<string> MissingAttributes { get; set; }
	}
}