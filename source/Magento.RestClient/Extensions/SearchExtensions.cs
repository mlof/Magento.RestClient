using System.Linq;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Search.Abstractions;

namespace Magento.RestClient.Extensions
{
	public static class SearchExtensions
	{
		public static AttributeSet GetDefaultAttributeSet(this ISearchService search,
			EntityType entityType = EntityType.CatalogProduct)
		{
			var response = search.AttributeSets(builder =>
				builder.WhereEquals(set => set.AttributeSetName, "Default")
					.WhereEquals(set => set.EntityTypeId, entityType)
					.WithPageSize(0));
			return response.Items.Single();
		}
	}
}