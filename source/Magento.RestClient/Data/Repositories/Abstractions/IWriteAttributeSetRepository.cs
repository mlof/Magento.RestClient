using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IWriteAttributeSetRepository
	{
		AttributeSet Create(EntityType entityTypeCode, long skeletonId, AttributeSet attributeSet);
	}
}