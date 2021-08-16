using FluentValidation;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Orders;

namespace Magento.RestClient.Validators
{
    internal class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
        }
    }
}