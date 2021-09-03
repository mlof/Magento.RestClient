using System;
using Magento.RestClient.Models.Carts;

namespace Magento.RestClient.Domain.Models
{
	public static class CartExtensions
	{
		public static CartModel SetPaymentMethod(this CartModel cart, PaymentMethod method)
		{
			return cart.SetPaymentMethod(method.Code);
		}

		/// <summary>
		///     Sets the shipping method for this cart.
		/// </summary>
		/// <param name="shippingMethod"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static CartModel SetShippingMethod(this CartModel cart, ShippingMethod shippingMethod)
		{
			return cart.SetShippingMethod(shippingMethod.CarrierCode, shippingMethod.MethodCode);
		}
	}
}