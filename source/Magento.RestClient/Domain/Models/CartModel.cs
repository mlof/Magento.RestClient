using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Data.Models.Carts.ConfigurableCartItems;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Validators;
using Magento.RestClient.Exceptions;
using Newtonsoft.Json;

namespace Magento.RestClient.Domain.Models
{
	public class CartModel : ICartModel
	{
		private readonly CartAddressValidator _addressValidator;
		private readonly CommitCartValidator _commitCartValidator;
		private readonly IAdminContext _context;
		private Address _billingAddress;
		private long _id;


		private Cart _model;

		private string _paymentMethod;
		private Address _shippingAddress;

		public CartModel(IAdminContext context)
		{
			_context = context;
			_addressValidator = new CartAddressValidator();
			_commitCartValidator = new CommitCartValidator();
			this.Id = context.Carts.GetNewCartId().GetAwaiter().GetResult();
		}


		public CartModel(IAdminContext context, long id)
		{
			_context = context;

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
					UpdateMagentoValues().GetAwaiter().GetResult();
				}
			}
		}


		public long? OrderId { get; private set; }

		public bool ShippingInformationSet { get; private set; }

		private async Task<CartModel> UpdateMagentoValues()
		{
			_model = await _context.Carts.GetExistingCart(_id);

			return this;
		}


		/// <summary>
		///     SetShippingMethod
		/// </summary>
		/// <param name="carrier"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public async Task<CartModel> SetShippingMethod(string carrier, string method)
		{
			if (this.ShippingAddress != null)
			{
				var estimatedShippingMethods =
					await _context.Carts.EstimateShippingMethods(this.Id, this.ShippingAddress);
				if (estimatedShippingMethods.Any(shippingMethod =>
					shippingMethod.MethodCode == method && shippingMethod.CarrierCode == carrier))
				{
					await _context.Carts.SetShippingInformation(this.Id, _shippingAddress, this.BillingAddress, carrier,
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
		public async Task<CartModel> SetPaymentMethod(string paymentMethod)
		{
			var paymentMethods = await _context.Carts.GetPaymentMethodsForCart(this.Id);
			if (paymentMethods.Any(method => method.Code == paymentMethod))
			{
				_paymentMethod = paymentMethod;
				return await UpdateMagentoValues();
			}

			throw new InvalidOperationException("Payment method is not valid for this cart.");
		}


		public async Task<CartModel> AddSimpleProduct(string sku, int quantity = 1)
		{
			//todo: Add configurable product functionality
			if (quantity > 0)
			{
				await _context.Carts.AddItemToCart(this.Id,
					new CartItem {Sku = sku, Qty = quantity, QuoteId = this.Id});
				return await UpdateMagentoValues();
			}

			throw new InvalidOperationException();
		}

		public async Task<List<ShippingMethod>> EstimateShippingMethods()
		{
			if (_shippingAddress == null)
			{
				throw new ArgumentNullException(nameof(this.ShippingAddress), "Set the shipping address first.");
			}

			if (!this.Items.Any())
			{
				throw new ArgumentNullException("Can not estimate shipping methods without items.");
			}

			return await _context.Carts.EstimateShippingMethods(this.Id, this.ShippingAddress);
		}


		public async Task<List<PaymentMethod>> GetPaymentMethods()
		{
			return await _context.Carts.GetPaymentMethodsForCart(this.Id);
		}

		/// <summary>
		///     Commits the cart and creates an order.
		/// </summary>
		/// <exception cref="CartCommittedException"></exception>
		/// <returns>Order ID</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public async Task<long> Commit()
		{
			_commitCartValidator.Validate(this, strategy => strategy.ThrowOnFailures());

			if (this.OrderId == null)
			{
				if (!this.ShippingInformationSet)
				{
					throw new InvalidOperationException("Set shipping information first.");
				}

				this.OrderId = await _context.Carts.PlaceOrder(this.Id, _paymentMethod, this.BillingAddress);
				return this.OrderId.Value;
			}

			throw new CartCommittedException(this.Id);
		}


		public async Task<CartModel> AssignCustomer(int customerId)
		{
			await _context.Carts.AssignCustomer(this.Id, _model.StoreId, customerId);

			return await UpdateMagentoValues();
		}

		public async Task AddConfigurableProduct(string parentSku, string childSku, int quantity = 1)
		{
			var options = await _context.ConfigurableProducts.GetOptions(parentSku);
			var children = await _context.ConfigurableProducts.GetConfigurableChildren(parentSku);
			var child = children.SingleOrDefault(product => product.Sku == childSku);

			var cartItem = new ConfigurableCartItem();
			cartItem.Sku = parentSku;
			cartItem.Qty = quantity;
			cartItem.QuoteId = this.Id;


			foreach (var option in options)
			{
				var s = child.CustomAttributes.SingleOrDefault(attribute => attribute.AttributeCode == option.Label);
				cartItem.ConfigurableItemOptions.Add(new ConfigurableItemOption {
					OptionId = option.AttributeId.ToString(), OptionValue = Convert.ToInt64(s.Value)
				});
			}

			await _context.Carts.AddItemToCart(this.Id, cartItem);
		}
	}
}