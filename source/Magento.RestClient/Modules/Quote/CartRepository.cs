using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Modules.Common.Models;
using Magento.RestClient.Modules.Customers.Models;
using Magento.RestClient.Modules.Quote.Abstractions;
using Magento.RestClient.Modules.Quote.Models;
using RestSharp;

namespace Magento.RestClient.Modules.Quote
{
	internal class CartRepository : AbstractRepository, ICartRepository
	{
		public CartRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public Task<Cart> GetExistingCart(long i)
		{
			var request = new RestRequest("carts/{id}");
			request.AddOrUpdateParameter("id", i, ParameterType.UrlSegment);
			return ExecuteAsync<Cart>(request);
		}

		public Task<CartItem> AddItemToCart(long cartId, CartItem cartItem)
		{
			var request = new RestRequest("carts/{id}/items", Method.Post);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {cartItem});
			return ExecuteAsync<CartItem>(request);
		}

		public Task<List<PaymentMethod>> GetPaymentMethodsForCart(long cartId)
		{
			var request = new RestRequest("carts/{id}/payment-methods");
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			return ExecuteAsync<List<PaymentMethod>>(request);
		}

		public async  Task<long?> PlaceOrder(long cartId, string paymentMethodCode, Address billingAddress)
		{
			var request = new RestRequest("carts/{id}/order", Method.Put);
			
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(
				new {paymentMethod = new {method = paymentMethodCode}});
			return await ExecuteAsync<long>(request).ConfigureAwait(false);
		}

		public Task<List<ShippingMethod>> EstimateShippingMethods(long cartId, Address address)
		{
			var request = new RestRequest("carts/{id}/estimate-shipping-methods", Method.Post);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {address});
			return ExecuteAsync<List<ShippingMethod>>(request);
		}

		public Task SetShippingInformation(long cartId, Address shippingAddress, Address billingAddress,
			string methodCode,
			string carrierCode, List<CustomAttribute> customAttributes = null)
		{
			var request = new RestRequest("carts/{id}/shipping-information", Method.Post);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {
				addressInformation = new {
					shipping_address = shippingAddress,
					billing_address = billingAddress,
					shipping_carrier_code = carrierCode,
					shipping_method_code = methodCode,
					custom_attributes = customAttributes
				}
			});
			return ExecuteAsync(request);
		}

		public Task AssignCustomer(long cartId, long storeId, long customerId)
		{
			var request = new RestRequest("carts/{id}", Method.Put);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {customerId, storeId});

			return ExecuteAsync(request);
		}

		public Task SetBillingAddress(long cartId, Address address)
		{
			var request = new RestRequest("carts/{id}/billing-address", Method.Post);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new { address});

			return ExecuteAsync(request);
		}

		public async  Task<long> GetNewCartId()
		{
			var request = new RestRequest("carts", Method.Post);
			request.SetScope("all");
			var response = await ExecuteAsync<string>(request).ConfigureAwait(false);
			var id = Convert.ToInt64(response);

			return id;
		}
	}
}