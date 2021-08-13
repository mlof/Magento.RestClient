using FluentValidation;
using Magento.RestClient.Models;

namespace Magento.RestClient.Validators
{
    public class CartAddressValidator : AbstractValidator<Address>
    {
        public CartAddressValidator()
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