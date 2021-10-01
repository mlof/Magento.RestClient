using System;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Domain.Models;

namespace Magento.RestClient.Domain.Extensions
{
	public static class CartExtensions
	{
		public static async Task<CartModel> SetPaymentMethod(this CartModel cart, PaymentMethod method)
		{
			return await cart.SetPaymentMethod(method.Code).ConfigureAwait(false);
		}

		/// <summary>
		///     Sets the shipping method for this cart.
		/// </summary>
		/// <param name="shippingMethod"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static async Task<CartModel> SetShippingMethod(this CartModel cart, ShippingMethod shippingMethod)
		{
			return await cart.SetShippingMethod(shippingMethod.CarrierCode, shippingMethod.MethodCode).ConfigureAwait(false);
		}
	}
}