using FluentValidation;
using Magento.RestClient.Models.Category;

namespace Magento.RestClient.Repositories
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