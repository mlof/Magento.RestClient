using System;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Category;
using Magento.RestClient.Domain.Models.Catalog;

namespace Magento.RestClient.Domain.Extensions
{
	public static class CategoryExtensions
	{
		public static CategoryModel ToModel(this Category category, IAdminContext context)
		{
			if (category.Id != 0)
			{
				return new CategoryModel(context, category.Id);
			}
			else
			{
				throw new InvalidOperationException("Cant get a category for Id 0");
			}
		}
	}
}