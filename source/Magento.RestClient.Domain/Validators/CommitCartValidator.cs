using FluentValidation;

namespace Magento.RestClient.Domain.Validators
{
    public class CommitCartValidator : AbstractValidator<Cart>
    {
        public CommitCartValidator()
        {
            RuleFor(cart => cart.OrderId);

        }
    }
}