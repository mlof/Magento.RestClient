using FluentValidation;

namespace Magento.RestClient.Domain.Validators
{
    public class CommitCartValidator : AbstractValidator<CartModel>
    {
        public CommitCartValidator()
        {
            RuleFor(cart => cart.OrderId);

        }
    }
}