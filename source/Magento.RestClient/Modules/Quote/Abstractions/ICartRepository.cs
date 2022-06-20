using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Modules.Common.Models;
using Magento.RestClient.Modules.Customers.Models;
using Magento.RestClient.Modules.Quote.Models;

namespace Magento.RestClient.Modules.Quote.Abstractions
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
			string carrierCode, List<CustomAttribute> customAttributes = null);

		Task AssignCustomer(long cartId, long storeId, long customerId);
		Task SetBillingAddress(long cartId, Address address);
	}
}