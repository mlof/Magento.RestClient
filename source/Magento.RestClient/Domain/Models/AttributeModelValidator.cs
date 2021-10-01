using FluentValidation;

namespace Magento.RestClient.Domain.Models
{
	public class AttributeModelValidator : AbstractValidator<AttributeModel>
	{
		public AttributeModelValidator()
		{
			RuleFor(model => model.DefaultFrontendLabel).NotEmpty();
			RuleFor(model => model.AttributeCode).Must(s =>
				!s.Contains("-"));
		}
	}
}