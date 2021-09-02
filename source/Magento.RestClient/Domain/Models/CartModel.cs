using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Magento.RestClient.Domain.Validators;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models.Carts;
using Magento.RestClient.Models.Carts.ConfigurableCartItems;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Customers;
using Magento.RestClient.Repositories.Abstractions;
using Newtonsoft.Json;

namespace Magento.RestClient.Domain.Models
{
	public class CartModel : ICartModel
	{
		private readonly CartAddressValidator _addressValidator;
		private readonly CommitCartValidator _commitCartValidator;
		private Address _billingAddress;
		private long _id;


		private Cart _model;

		private string _paymentMethod;
		private Address _shippingAddress;
		private readonly IAdminClient client;

		public CartModel(IAdminClient client)
		{
			this.client = client;
			_addressValidator = new CartAddressValidator();
			_commitCartValidator = new CommitCartValidator();
			this.Id = client.Carts.GetNewCartId();
		}


		public CartModel(IAdminClient client, long id)
		{
			this.client = client;

			_addressValidator = new CartAddressValidator();
			_commitCartValidator = new CommitCartValidator();

			this.Id = id;
		}

		public Customer Customer => _model.Customer;

		public List<CartItem> Items => _model.Items;

		public Address BillingAddress {
			get => _billingAddress;
			set {
				_addressValidator.ValidateAndThrow(value);
				_billingAddress = value;
			}
		}

		public Address ShippingAddress {
			get => _shippingAddress;
			set {
				_addressValidator.ValidateAndThrow(value);
				_shippingAddress = value;
			}
		}


		[JsonProperty("id")]
		public long Id {
			get => _id;
			private set {
				_id = value;
				if (_id > 0)
				{
					UpdateMagentoValues();
				}
			}
		}


		public long? OrderId { get; private set; }

		public bool ShippingInformationSet { get; private set; }

		private CartModel UpdateMagentoValues()
		{
			_model = client.Carts.GetExistingCart(_id);

			return this;
		}


		/// <summary>
		///     SetShippingMethod
		/// </summary>
		/// <param name="carrier"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public CartModel SetShippingMethod(string carrier, string method)
		{
			if (this.ShippingAddress != null)
			{
				if (client.Carts.EstimateShippingMethods(this.Id, this.ShippingAddress).Any(shippingMethod =>
					shippingMethod.MethodCode == method && shippingMethod.CarrierCode == carrier))
				{
					client.Carts.SetShippingInformation(this.Id, _shippingAddress, this.BillingAddress, carrier,
						method);
					this.ShippingInformationSet = true;
				}
				else
				{
					throw new InvalidOperationException("Shipping method is invalid for this address.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(this.ShippingAddress),
					"Can not set shipping method without a shipping address.");
			}

			return this;
		}


		/// <summary>
		///     Sets the payment method for this cart.
		/// </summary>
		/// <param name="paymentMethod"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public CartModel SetPaymentMethod(string paymentMethod)
		{
			var paymentMethods = client.Carts.GetPaymentMethodsForCart(this.Id);
			if (paymentMethods.Any(method => method.Code == paymentMethod))
			{
				_paymentMethod = paymentMethod;
				return UpdateMagentoValues();
			}

			throw new InvalidOperationException("Payment method is not valid for this cart.");
		}


		public CartModel AddSimpleProduct(string sku, int quantity)
		{
			//todo: Add configurable product functionality
			if (quantity > 0)
			{
				client.Carts.AddItemToCart(this.Id, new CartItem() {Sku = sku, Qty = quantity, QuoteId = this.Id});
				return UpdateMagentoValues();
			}

			throw new InvalidOperationException();
		}

		public List<ShippingMethod> EstimateShippingMethods()
		{
			if (_shippingAddress == null)
			{
				throw new ArgumentNullException(nameof(this.ShippingAddress), "Set the shipping address first.");
			}

			if (!this.Items.Any())
			{
				throw new ArgumentNullException("Can not estimate shipping methods without items.");
			}

			return client.Carts.EstimateShippingMethods(this.Id, this.ShippingAddress);
		}


		public List<PaymentMethod> GetPaymentMethods()
		{
			return client.Carts.GetPaymentMethodsForCart(this.Id);
		}

		/// <summary>
		///     Commits the cart and creates an order.
		/// </summary>
		/// <exception cref="CartAlreadyCommittedException"></exception>
		/// <returns>Order ID</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public long Commit()
		{
			_commitCartValidator.Validate(this, strategy => strategy.ThrowOnFailures());

			if (this.OrderId == null)
			{
				if (!this.ShippingInformationSet)
				{
					throw new InvalidOperationException("Set shipping information first.");
				}

				this.OrderId = client.Carts.PlaceOrder(this.Id, _paymentMethod, this.BillingAddress);
				return this.OrderId.Value;
			}

			throw new CartAlreadyCommittedException(this.Id);
		}

		/// <summary>
		///     Sets the shipping method for this cart.
		/// </summary>
		/// <param name="shippingMethod"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public CartModel SetShippingMethod(ShippingMethod shippingMethod)
		{
			return SetShippingMethod(shippingMethod.CarrierCode, shippingMethod.MethodCode);
		}

		public CartModel AssignCustomer(int customerId)
		{
			client.Carts.AssignCustomer(this.Id, _model.StoreId, customerId);

			return UpdateMagentoValues();
		}

		public void AddConfigurableProduct(string parentSku, string childSku, int quantity = 1)
		{
			var options = client.ConfigurableProducts.GetOptions(parentSku);
			var children = client.ConfigurableProducts.GetConfigurableChildren(parentSku);
			var child = children.SingleOrDefault(product => product.Sku == childSku);

			var cartItem = new ConfigurableCartItem();
			cartItem.Sku = parentSku;
			cartItem.Qty = quantity;
			cartItem.QuoteId = Id;


			foreach (var option in options)
			{

				var s = child.CustomAttributes.SingleOrDefault(attribute => attribute.AttributeCode == option.Label);
				cartItem.ConfigurableItemOptions.Add(new ConfigurableItemOption() {
					OptionId = option.AttributeId.ToString(),
					OptionValue = Convert.ToInt64(s.Value)
				});
			}

			client.Carts.AddItemToCart(Id, cartItem);
		}
	}
}