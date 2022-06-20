using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.EAV.Model;

namespace Magento.RestClient.Modules.EAV.Abstractions
{
	public interface IAttributeSetRepository : IHasQueryable<AttributeSet>
	{
		Task<AttributeSet> Create(EntityType entityTypeCode, long skeletonId, AttributeSet attributeSet);

		Task Delete(long attributeSetId);

		Task AssignProductAttribute(long attributeSetId, long attributeGroupId, string attributeCode,
			int sortOrder = 1);

		Task<long> CreateProductAttributeGroup(long attributeSetId, string attributeGroupName);
		Task<AttributeSet> Get(long id);
	}
}