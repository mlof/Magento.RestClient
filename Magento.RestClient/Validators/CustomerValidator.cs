using FluentValidation;
using Magento.RestClient.Models;

namespace Magento.RestClient.Validators
{
    class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Email).EmailAddress();
            
        }
    }
}