using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ICartRepository
	{
		Task<long> GetNewCartId();
		Task<Cart> GetExistingCart(long i);
		Task<CartItem> AddItemToCart(long cartId, CartItem cartItem);
		Task<List<PaymentMethod>> GetPaymentMethodsForCart(long cartId);
		Task<long?> PlaceOrder(long cartId, string paymentMethodCode, Address billingAddress);
		Task<List<ShippingMethod>> EstimateShippingMethods(long cartId, Address address);

		Task SetShippingInformation(long cartId, Address shippingAddress, Address billingAddress, string methodCode,
			string carrierCode);

		Task AssignCustomer(long cartId, long storeId, int customerId);
	}
}