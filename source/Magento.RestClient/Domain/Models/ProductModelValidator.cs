using FluentValidation;

namespace Magento.RestClient.Domain.Models
{
	public class ProductModelValidator : AbstractValidator<ProductModel>

	{
		public ProductModelValidator()
		{
			RuleFor(product => product.Sku).NotEmpty();
			RuleFor(product => product.Price).NotEmpty();
			RuleFor(product => product.AttributeSetId).NotEmpty();
			RuleFor(product => product.Price).NotNull();
			RuleFor(product => product.Name).NotEmpty();
		}
	}
}