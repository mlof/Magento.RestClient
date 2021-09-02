using System;

namespace Magento.RestClient.Exceptions
{
	public class CartAlreadyCommittedException : Exception
	{
		public CartAlreadyCommittedException(long id) : base(
			$"Cart with ID: {id} has already been committed. Try getting the Order ID instead.")
		{
		}
	}
}