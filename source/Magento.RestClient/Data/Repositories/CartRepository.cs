﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class CartRepository : AbstractRepository, ICartRepository
	{
		private readonly IRestClient _client;

		public CartRepository(IRestClient client)
		{
			_client = client;
		}

		async public Task<Cart> GetExistingCart(long i)
		{
			var request = new RestRequest("carts/{id}");
			request.Method = Method.GET;
			request.AddOrUpdateParameter("id", i, ParameterType.UrlSegment);
			var response = await _client.ExecuteAsync<Cart>(request);


			return response.Data;
		}


		async public Task<CartItem> AddItemToCart(long cartId, CartItem cartItem)
		{
			var request = new RestRequest("carts/{id}/items");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {cartItem = cartItem});
			var response = await _client.ExecuteAsync<CartItem>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}

		async public Task<List<PaymentMethod>> GetPaymentMethodsForCart(long cartId)
		{
			var request = new RestRequest("carts/{id}/payment-methods");
			request.Method = Method.GET;
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			var response = await _client.ExecuteAsync<List<PaymentMethod>>(request);
			return response.Data;
		}

		async public Task<long?> PlaceOrder(long cartId, string paymentMethodCode, Address billingAddress)
		{
			var request = new RestRequest("carts/{id}/order");
			request.Method = Method.PUT;
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(
				new {paymentMethod = new {method = paymentMethodCode}, address = billingAddress});
			var response = await _client.ExecuteAsync<long>(request);
			return response.Data;
		}

		async public Task<List<ShippingMethod>> EstimateShippingMethods(long cartId, Address address)
		{
			var request = new RestRequest("carts/{id}/estimate-shipping-methods");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {address});
			var response = await _client.ExecuteAsync<List<ShippingMethod>>(request);
			return response.Data;
		}

		async public Task SetShippingInformation(long cartId, Address shippingAddress, Address billingAddress,
			string methodCode,
			string carrierCode)
		{
			var request = new RestRequest("carts/{id}/shipping-information");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {
				addressInformation = new {
					shipping_address = shippingAddress,
					billing_address = billingAddress,
					shipping_carrier_code = carrierCode,
					shipping_method_code = methodCode
				}
			});
			var response = await _client.ExecuteAsync(request);
		}

		/// <summary>
		///     AssignCustomer
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="storeId"></param>
		/// <param name="customerId"></param>
		/// <exception cref="EntityNotFoundException"></exception>
		async public Task AssignCustomer(long cartId, long storeId, int customerId)
		{
			var request = new RestRequest("carts/{id}");
			request.Method = Method.PUT;
			request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
			request.AddJsonBody(new {customerId, storeId});


			var response = await _client.ExecuteAsync(request);
			if (!response.IsSuccessful)
			{
				if (response.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException(typeof(Customer), customerId.ToString());
				}
			}
		}

		async public Task<long> GetNewCartId()
		{
			var request = new RestRequest("carts");
			request.Method = Method.POST;
			var response = await _client.ExecuteAsync<string>(request);
			var id = Convert.ToInt64(response.Data);

			return id;
		}
	}
}