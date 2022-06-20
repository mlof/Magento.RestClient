using FluentValidation;
using Magento.RestClient.Modules.Customers.Models;

namespace Magento.RestClient.Modules.Customers.Validation
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