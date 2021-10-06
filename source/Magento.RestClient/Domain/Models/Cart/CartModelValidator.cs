using FluentValidation;

namespace Magento.RestClient.Domain.Models.Cart
{
	public class CartModelValidator : AbstractValidator<CartModel>
	{
		public CartModelValidator()
		{
			RuleFor(model => model.BillingAddress).SetValidator(new CartModelAddressValidator());
			RuleFor(model => model.ShippingAddress).SetValidator(new CartModelAddressValidator());
		}
	}
}