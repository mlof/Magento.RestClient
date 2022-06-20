using FluentValidation;
using Magento.RestClient.Modules.Sales.Models;

namespace Magento.RestClient.Modules.Quote.Validation
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