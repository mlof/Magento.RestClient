using FluentValidation;
using Magento.RestClient.Modules.Order.Models;

namespace Magento.RestClient.Modules.Quote.Validation
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