using System;
using System.Collections.Generic;
using System.Net;
using Magento.RestClient.Domain;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Carts;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;
using Customer = Magento.RestClient.Models.Customers.Customer;

namespace Magento.RestClient.Repositories
{
    internal class CartRepository : AbstractRepository, ICartRepository
    {
        private readonly IRestClient _client;

        public CartRepository(IRestClient client)
        {
            this._client = client;
        }

        public Cart GetExistingCart(long i)
        {
            var request = new RestRequest("carts/{id}");
            request.Method = Method.GET;
            request.AddOrUpdateParameter("id", i, ParameterType.UrlSegment);
            var response = _client.Execute<Cart>(request);


            return response.Data;
        }

        /// <summary>
        /// AddItemToCart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// <exception cref="MagentoException"></exception>
        public CartItem AddItemToCart(long cartId, string sku, int quantity)
        {
            var request = new RestRequest("carts/{id}/items");
            request.Method = Method.POST;
            request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
            request.AddJsonBody(new {cartItem = new {sku, qty = quantity, quoteId = cartId.ToString()}});
            var response = _client.Execute<CartItem>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                
                throw MagentoException.Parse(response.Content);
            }
        }

        public List<PaymentMethod> GetPaymentMethodsForCart(long cartId)
        {
            var request = new RestRequest("carts/{id}/payment-methods");
            request.Method = Method.GET;
            request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
            var response = _client.Execute<List<PaymentMethod>>(request);
            return response.Data;
        }

        public long? PlaceOrder(long cartId, string paymentMethodCode, Address billingAddress)
        {
            var request = new RestRequest("carts/{id}/order");
            request.Method = Method.PUT;
            request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
            request.AddJsonBody(
                new {paymentMethod = new {method = paymentMethodCode}, address = billingAddress});
            var response = _client.Execute<long>(request);
            return response.Data;

        }

        public List<ShippingMethod> EstimateShippingMethods(long cartId, Address address)
        {
            var request = new RestRequest("carts/{id}/estimate-shipping-methods");
            request.Method = Method.POST;
            request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
            request.AddJsonBody(new {address});
            var response = _client.Execute<List<ShippingMethod>>(request);
            return response.Data;
        }

        public void SetShippingInformation(long cartId, Address shippingAddress, Address billingAddress,
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
            var response = _client.Execute(request);
        }

        /// <summary>
        /// AssignCustomer
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="storeId"></param>
        /// <param name="customerId"></param>
        /// <exception cref="EntityNotFoundException"></exception>
        public void AssignCustomer(long cartId, long storeId, int customerId)
        {
            var request = new RestRequest("carts/{id}");
            request.Method = Method.PUT;
            request.AddOrUpdateParameter("id", cartId, ParameterType.UrlSegment);
            request.AddJsonBody(new {customerId, storeId});
         
            
            var response = _client.Execute(request);
            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException(typeof(Customer), customerId.ToString());
                }
            }
        }

        public long GetNewCartId()
        {
            var request = new RestRequest("carts");
            request.Method = Method.POST;
            var response = _client.Execute<string>(request);
            var id = Convert.ToInt64(response.Data);

            return id;
        }
    }
}