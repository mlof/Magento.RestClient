using System.Linq;
using Magento.RestClient.Modules.EAV.Abstractions;
using Magento.RestClient.Modules.EAV.Model;

namespace Magento.RestClient.Modules.EAV
{
	public static class AttributeSetRepositoryExtensions
	{
		public static AttributeSet GetDefaultAttributeSet(this IAttributeSetRepository attributeSets,
			EntityType entityType = EntityType.CatalogProduct)
		{
			return attributeSets.AsQueryable().Single(set =>
				set.AttributeSetName == "Default" && set.EntityTypeId == entityType);
		}
	}
}