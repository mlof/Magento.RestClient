using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Domain;
using Magento.RestClient.Data.Models.Carts;
using Magento.RestClient.Data.Models.Carts.ConfigurableCartItems;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Domain.Exceptions;
using Magento.RestClient.Domain.Models.Cart.Exceptions;
using Newtonsoft.Json;

namespace Magento.RestClient.Domain.Models.Cart
{
	public class CartModel : ICartModel
	{
		private readonly IAdminContext _context;
		private long _id;

		private Data.Models.Carts.Cart _model;

		private string _paymentMethod;
		private readonly CartModelValidator _validator;

		public CartModel(IAdminContext context)
		{
			_context = context;
			_validator = new CartModelValidator();
			this.Id = context.Carts.GetNewCartId().GetAwaiter().GetResult();
		}

		public CartModel(IAdminContext context, long id)
		{
			_context = context;

			this.Id = id;
		}

		public Customer Customer => _model.Customer;

		public List<CartItem> Items => _model.Items;

		public Address BillingAddress { get; set; }

		public Address ShippingAddress { get; set; }

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
			_model = await _context.Carts.GetExistingCart(_id).ConfigureAwait(false);

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
					await _context.Carts.EstimateShippingMethods(this.Id, this.ShippingAddress).ConfigureAwait(false);
				if (estimatedShippingMethods.Any(shippingMethod =>
					shippingMethod.MethodCode == method && shippingMethod.CarrierCode == carrier))
				{
					await _context.Carts.SetShippingInformation(this.Id, this.ShippingAddress, this.BillingAddress,
						carrier,
						method).ConfigureAwait(false);
					this.ShippingInformationSet = true;
				}
				else
				{
					throw new InvalidOperationException("Shipping method is invalid for this address.");
				}
			}
			else
			{
				throw new CartValidationException(nameof(this.ShippingAddress),
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
			var paymentMethods = await _context.Carts.GetPaymentMethodsForCart(this.Id).ConfigureAwait(false);
			if (paymentMethods.Any(method => method.Code == paymentMethod))
			{
				_paymentMethod = paymentMethod;
				return await UpdateMagentoValues().ConfigureAwait(false);
			}

			throw new InvalidOperationException("Payment method is not valid for this cart.");
		}

		public async Task<CartModel> AddSimpleProduct(string sku, int quantity = 1)
		{
			if (quantity > 0)
			{
				await _context.Carts.AddItemToCart(this.Id,
					new CartItem { Sku = sku, Qty = quantity, QuoteId = this.Id }).ConfigureAwait(false);
				return await UpdateMagentoValues().ConfigureAwait(false);
			}

			throw new InvalidOperationException();
		}

		public async Task<List<ShippingMethod>> EstimateShippingMethods()
		{
			ValidateEstimateShippingMethods();

			return await _context.Carts.EstimateShippingMethods(this.Id, this.ShippingAddress).ConfigureAwait(false);
		}

		private void ValidateEstimateShippingMethods()
		{
			if (this.ShippingAddress == null)
			{
				throw new CartValidationException(nameof(this.ShippingAddress), "Set the shipping address first.");
			}

			if (!this.Items.Any())
			{
				throw new CartValidationException("Can not estimate shipping methods without items.");
			}
		}

		public async Task<List<PaymentMethod>> GetPaymentMethods()
		{
			return await _context.Carts.GetPaymentMethodsForCart(this.Id).ConfigureAwait(false);
		}

		/// <summary>
		///     Commits the cart and creates an order.
		/// </summary>
		/// <exception cref="CartCommittedException"></exception>
		/// <returns>Order ID</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public async Task<long> Commit()
		{
			await _validator.ValidateAndThrowAsync(this);

			if (this.OrderId == null)
			{
				if (!this.ShippingInformationSet)
				{
					throw new InvalidOperationException("Set shipping information first.");
				}

				this.OrderId = await _context.Carts.PlaceOrder(this.Id, _paymentMethod, this.BillingAddress)
					.ConfigureAwait(false);
				return this.OrderId.Value;
			}

			throw new CartCommittedException(this.Id);
		}

		public async Task<CartModel> AssignCustomer(int customerId)
		{
			await _context.Carts.AssignCustomer(this.Id, _model.StoreId, customerId).ConfigureAwait(false);

			return await UpdateMagentoValues().ConfigureAwait(false);
		}

		public async Task AddConfigurableProduct(string parentSku, string childSku, int quantity = 1)
		{
			var options = await _context.ConfigurableProducts.GetOptions(parentSku).ConfigureAwait(false);
			var children = await _context.ConfigurableProducts.GetConfigurableChildren(parentSku).ConfigureAwait(false);
			var child = children.SingleOrDefault(product => product.Sku == childSku);

			var cartItem = new ConfigurableCartItem { Sku = parentSku, Qty = quantity, QuoteId = this.Id };

			foreach (var option in options)
			{
				var s = child.CustomAttributes.SingleOrDefault(attribute => attribute.AttributeCode == option.Label);
				cartItem.ConfigurableItemOptions.Add(new ConfigurableItemOption
				{
					OptionId = option.AttributeId.ToString(),
					OptionValue = Convert.ToInt64(s.Value)
				});
			}

			await _context.Carts.AddItemToCart(this.Id, cartItem).ConfigureAwait(false);
		}
	}
}