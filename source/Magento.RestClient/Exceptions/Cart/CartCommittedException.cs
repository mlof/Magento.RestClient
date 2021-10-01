using System;

namespace Magento.RestClient.Exceptions
{
	public class CartCommittedException : Exception
	{
		public CartCommittedException(long id) : base(
			$"Cart with ID: {id} has already been committed. Try getting the Order ID instead.")
		{
		}

		public CartCommittedException() : base()
		{
		}

		public CartCommittedException(string message) : base(message)
		{
		}

		public CartCommittedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}