using System.Collections.Generic;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ICartRepository
	{
		long GetNewCartId();
		Cart GetExistingCart(long i);
		CartItem AddItemToCart(long cartId, CartItem cartItem);
		List<PaymentMethod> GetPaymentMethodsForCart(long cartId);
		long? PlaceOrder(long cartId, string paymentMethodCode, Address billingAddress);
		List<ShippingMethod> EstimateShippingMethods(long cartId, Address address);

		void SetShippingInformation(long cartId, Address shippingAddress, Address billingAddress, string methodCode,
			string carrierCode);

		void AssignCustomer(long cartId, long storeId, int customerId);
	}
}