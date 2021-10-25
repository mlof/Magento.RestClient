using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Exceptions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class CartRepository : AbstractRepository, ICartRepository
	{
		public CartRepository(IContext context) : base(context)
		{
		}

		public Task<Cart> GetExistingCart(long i)
		{
			var request = new RestRequest("carts/{id}", Method.GET);
			request.AddOrUpdateParameter("id", i, ParameterType.UrlSegment);
			return ExecuteAsync<Cart>(request);
		}

		public Task<CartItem> AddItemToCart(long cartId, CartItem cartItem)
		{
			var request = new RestRequest("carts/{id}/items", Method.POST);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {cartItem = cartItem});
			return ExecuteAsync<CartItem>(request);
		}

		public Task<List<PaymentMethod>> GetPaymentMethodsForCart(long cartId)
		{
			var request = new RestRequest("carts/{id}/payment-methods", Method.GET);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			return ExecuteAsync<List<PaymentMethod>>(request);
		}

		public async Task<long?> PlaceOrder(long cartId, string paymentMethodCode, Address billingAddress)
		{
			var request = new RestRequest("carts/{id}/order", Method.PUT);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(
				new {paymentMethod = new {method = paymentMethodCode}, address = billingAddress});
			return await ExecuteAsync<long>(request).ConfigureAwait(false);
		}

		public Task<List<ShippingMethod>> EstimateShippingMethods(long cartId, Address address)
		{
			var request = new RestRequest("carts/{id}/estimate-shipping-methods", Method.POST);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {address});
			return ExecuteAsync<List<ShippingMethod>>(request);
		}

		public Task SetShippingInformation(long cartId, Address shippingAddress, Address billingAddress,
			string methodCode,
			string carrierCode)
		{
			var request = new RestRequest("carts/{id}/shipping-information", Method.POST);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {
				addressInformation = new {
					shipping_address = shippingAddress,
					billing_address = billingAddress,
					shipping_carrier_code = carrierCode,
					shipping_method_code = methodCode
				}
			});
			return ExecuteAsync(request);
		}

		public Task AssignCustomer(long cartId, long storeId, long customerId)
		{
			var request = new RestRequest("carts/{id}", Method.PUT);
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {customerId, storeId});

			return ExecuteAsync(request);
		}

		public async Task<long> GetNewCartId()
		{
			var request = new RestRequest("carts", Method.POST);
			var response =  await ExecuteAsync<string>(request).ConfigureAwait(false);
			var id = Convert.ToInt64(response);

			return id;
		}
	}
}