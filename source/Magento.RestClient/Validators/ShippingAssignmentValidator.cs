using FluentValidation;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Shipping;

namespace Magento.RestClient.Validators
{
    internal class ShippingAssignmentValidator : AbstractValidator<ShippingAssignment>
    {
        public ShippingAssignmentValidator()
        {
            RuleFor(assignment => assignment.Shipping).NotNull();
            RuleFor(assignment => assignment.Shipping.Address).NotNull()
                .When(assignment => assignment.Shipping != null);

            RuleFor(assignment => assignment.Shipping.Address).SetValidator(new OrderAddressValidator())
                .When(assignment => assignment.Shipping != null);
        }
    }
}