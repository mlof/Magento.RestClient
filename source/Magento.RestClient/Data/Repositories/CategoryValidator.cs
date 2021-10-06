using FluentValidation;
using Magento.RestClient.Data.Models.Catalog.Category;

namespace Magento.RestClient.Data.Repositories
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