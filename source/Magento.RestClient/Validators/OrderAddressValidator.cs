using FluentValidation;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Orders;

namespace Magento.RestClient.Validators
{
    internal class OrderAddressValidator : AbstractValidator<OrderAddress>
    {
        public OrderAddressValidator()
        {
            RuleFor(address => address.City).NotEmpty();
            RuleFor(address => address.CountryId).NotEmpty();
            RuleFor(address => address.Firstname).NotEmpty();
            RuleFor(address => address.Lastname).NotEmpty();
            RuleFor(address => address.Postcode).NotEmpty();


            RuleFor(address => address.Street).NotEmpty();
            RuleFor(address => address.Telephone).NotEmpty();
        }
    }
}