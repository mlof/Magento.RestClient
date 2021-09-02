using Magento.RestClient.Models.Attributes;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface IAttributeSetRepository : IReadAttributeSetRepository, IWriteAttributeSetRepository
	{
		void Delete(long attributeSetId);

		void AssignProductAttribute(long attributeSetId, long attributeGroupId, string attributeCode,
			int sortOrder = 1);

		long CreateProductAttributeGroup(long attributeSetId, string attributeGroupName);
		AttributeSet Get(long id);
	}
}