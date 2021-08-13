using FluentValidation;
using Magento.RestClient.Models;

namespace Magento.RestClient.Validators
{
    internal class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
        }
    }
}