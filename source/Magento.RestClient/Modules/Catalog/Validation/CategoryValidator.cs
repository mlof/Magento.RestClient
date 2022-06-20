using FluentValidation;
using Magento.RestClient.Modules.Catalog.Models.Category;

namespace Magento.RestClient.Modules.Catalog.Validation
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(category => category.Name).NotEmpty();
			RuleFor(category => category.IsActive).NotNull();
		}
	}
}