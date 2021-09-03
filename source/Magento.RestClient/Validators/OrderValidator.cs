using FluentValidation;
using Magento.RestClient.Data.Models.Orders;

namespace Magento.RestClient.Validators
{
	internal class OrderValidator : AbstractValidator<Order>
	{
		public OrderValidator()
		{
			RuleFor(order => order.CustomerEmail).NotEmpty();
			RuleFor(order => order.Payment).NotNull();
			RuleFor(order => order.BillingAddress).SetValidator(order => new OrderAddressValidator())
				.When(order => order.BillingAddress != null);
			RuleFor(order => order.CustomerEmail).EmailAddress();
			RuleFor(order => order.ExtensionAttributes)
				.NotNull();
			RuleFor(order => order.ExtensionAttributes.ShippingAssignments)
				.NotNull()
				.When(order => order.ExtensionAttributes != null);
			RuleForEach(order => order.ExtensionAttributes.ShippingAssignments)
				.SetValidator(order => new ShippingAssignmentValidator())
				.When(order => order.ExtensionAttributes?.ShippingAssignments != null);
			RuleFor(order => order.Payment.Method).NotNull().When(order => order.Payment != null);

			RuleForEach(order => order.Items).SetValidator(new OrderItemValidator());
		}
	}
}