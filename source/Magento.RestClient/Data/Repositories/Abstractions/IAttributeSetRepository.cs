using System.Linq;
using Magento.RestClient.Data.Models.Attributes;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IAttributeSetRepository : IReadAttributeSetRepository, IWriteAttributeSetRepository, IQueryable<AttributeSet>
	{
		void Delete(long attributeSetId);

		void AssignProductAttribute(long attributeSetId, long attributeGroupId, string attributeCode,
			int sortOrder = 1);

		long CreateProductAttributeGroup(long attributeSetId, string attributeGroupName);
		AttributeSet Get(long id);
	}
}