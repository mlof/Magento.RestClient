using FluentValidation;
using Magento.RestClient.Models;

namespace Magento.RestClient.Validators
{
    class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Sku).NotEmpty();
            RuleFor(product => product.Price).NotEmpty();
            RuleFor(product => product.AttributeSetId).NotEmpty();
            RuleFor(product => product.Price).GreaterThanOrEqualTo(0);
        }
    }
}