using System;

namespace Magento.RestClient.Domain.Models.Cart.Exceptions
{
	public class CartValidationException : Exception
	{
		public CartValidationException(string shippingAddressName,
			string canNotSetShippingMethodWithoutAShippingAddress)
		{
		}

		public CartValidationException() : base()
		{
		}

		public CartValidationException(string message) : base(message)
		{
		}

		public CartValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}