using FluentValidation;
using Magento.RestClient.Models.Products;

namespace Magento.RestClient.Domain.Validators
{
	internal class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{
			RuleFor(product => product.Sku).NotEmpty();
			RuleFor(product => product.Price).NotEmpty();
			RuleFor(product => product.AttributeSetId).NotEmpty();
			RuleFor(product => product.Price).NotNull();
			RuleFor(product => product.Name).NotEmpty();
		}
	}
}