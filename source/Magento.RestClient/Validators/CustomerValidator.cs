using FluentValidation;
using Magento.RestClient.Models.Customers;

namespace Magento.RestClient.Validators
{
	internal class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{
			RuleFor(customer => customer.Email).NotEmpty();
			RuleFor(customer => customer.Email).EmailAddress();
		}
	}
}