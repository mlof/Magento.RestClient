using FluentValidation;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Domain.Models
{
	public class CartModelAddressValidator : AbstractValidator<Address>
	{
		public CartModelAddressValidator()
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