using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Validators;
using Newtonsoft.Json;

namespace Magento.RestClient.Domain
{
    public class Cart
    {
        private readonly CartAddressValidator _addressValidator;
        private readonly ICartRepository _cartRepository;
        private readonly CommitCartValidator _commitCartValidator;
        private Address _billingAddress;
        private long _id;


        private CartModel _model;

        private string _paymentMethod;
        private Address _shippingAddress;

        public Customer Customer => _model.Customer;

        public List<CartItem> Items => _model.Items;

        public Cart(ICartRepository cartRepository)
        {
            _addressValidator = new CartAddressValidator();
            _commitCartValidator = new CommitCartValidator();
            _cartRepository = cartRepository;
        }

        public Address BillingAddress {
            get { return _billingAddress; }
            set {
                _addressValidator.ValidateAndThrow(value);
                _billingAddress = value;
            }
        }

        public Address ShippingAddress {
            get { return _shippingAddress; }
            set {
                _addressValidator.ValidateAndThrow(value);
                _shippingAddress = value;
            }
        }


        [JsonProperty("id")]
        public long Id {
            get { return _id; }
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

        private Cart UpdateMagentoValues()
        {
            _model = _cartRepository.GetExistingCart(_id);

            return this;
        }


        public static Cart CreateNew(ICartRepository repository)
        {
            var cart = new Cart(repository);
            cart.CreateNew();
            return cart;
        }

        /// <summary>
        /// SetShippingMethod
        /// </summary>
        /// <param name="carrier"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Cart SetShippingMethod(string carrier, string method)
        {
            if (this.ShippingAddress != null)
            {
                if (_cartRepository.EstimateShippingMethods(this.Id, this.ShippingAddress).Any(shippingMethod =>
                    shippingMethod.MethodCode == method && shippingMethod.CarrierCode == carrier))
                {
                    _cartRepository.SetShippingInformation(this.Id, _shippingAddress, this.BillingAddress, carrier,
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

        public static Cart GetExisting(ICartRepository repository, long cartId)
        {
            var cart = new Cart(repository) {Id = cartId};
            return cart;
        }

        public Cart SetPaymentMethod(string paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return UpdateMagentoValues();
        }


        private void CreateNew()
        {
            this.Id = _cartRepository.GetNewCartId();
        }

        public Cart AddItem(string sku, int quantity)
        {
            if (quantity > 0)
            {
                _cartRepository.AddItemToCart(this.Id, sku, quantity);
                return UpdateMagentoValues();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public List<ShippingMethod> EstimateShippingMethods()
        {
            if (_shippingAddress == null)
            {
                throw new ArgumentNullException(nameof(this.ShippingAddress), "Set the shipping address first.");
            }

            return _cartRepository.EstimateShippingMethods(this.Id, this.ShippingAddress);
        }


        public List<PaymentMethod> GetPaymentMethods()
        {
            return _cartRepository.GetPaymentMethodsForCart(this.Id);
        }

        /// <summary>
        /// </summary>
        /// <exception cref="CartAlreadyCommittedException"></exception>
        /// <returns>Order ID</returns>
        public long Commit()
        {
            _commitCartValidator.Validate(this, strategy => strategy.ThrowOnFailures());

            if (this.OrderId == null)
            {
                if (!this.ShippingInformationSet)
                {
                    throw new InvalidOperationException("Set shipping information first.");
                }

                this.OrderId = _cartRepository.PlaceOrder(this.Id, _paymentMethod, this.BillingAddress);
                return this.OrderId.Value;
            }

            throw new CartAlreadyCommittedException(Id);
        }

        /// <summary>
        /// Sets the shipping method for this cart.
        /// </summary>
        /// <param name="shippingMethod"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Cart SetShippingMethod(ShippingMethod shippingMethod)
        {
            return SetShippingMethod(shippingMethod.CarrierCode, shippingMethod.MethodCode);
        }

        public Cart AssignCustomer(int customerId)
        {
            _cartRepository.AssignCustomer(this.Id, _model.StoreId, customerId);

            return UpdateMagentoValues();
        }
    }
}