using FluentValidation;

namespace Magento.RestClient.Domain
{
	internal class ProductValidator : AbstractValidator<Models.Products.Product>
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