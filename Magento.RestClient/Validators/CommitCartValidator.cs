using FluentValidation;
using Magento.RestClient.Domain;

namespace Magento.RestClient.Validators
{
    public class CommitCartValidator : AbstractValidator<Cart>
    {
        public CommitCartValidator()
        {
            RuleFor(cart => cart.OrderId);

        }
    }
}