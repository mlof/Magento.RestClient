using System;
using System.Collections.Generic;

namespace Magento.RestClient.Exceptions
{
	public class InvalidChildProductException : Exception
	{
		public InvalidChildProductException() : base()
		{
		}

		public InvalidChildProductException(string message) : base(message)
		{
		}
		public InvalidChildProductException(string message, Exception e) : base(message, e)
		{
		}

		public List<string> MissingAttributes { get; set; }
	}
}