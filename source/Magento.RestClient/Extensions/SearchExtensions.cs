using System.Linq;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Repositories.Abstractions;

namespace Magento.RestClient.Extensions
{
	public static class SearchExtensions
	{
		public static AttributeSet GetDefaultAttributeSet(this IAttributeSetRepository attributeSets,
			EntityType entityType = EntityType.CatalogProduct)
		{
			return attributeSets.AsQueryable().Single(set =>
				set.AttributeSetName == "Default" && set.EntityTypeId == entityType);
		}
	}
}