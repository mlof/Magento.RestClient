using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IAttributeRepository
	{
		Task<IEnumerable<EntityAttribute>> GetProductAttributes(long attributeSetId);

		Task<ProductAttribute> Create(ProductAttribute attribute);
		Task DeleteProductAttribute(string attributeCode);
		Task<List<Option>> GetProductAttributeOptions(string attributeCode);
		Task<int> CreateProductAttributeOption(string attributeCode, Option option);
		Task<ProductAttribute> GetByCode(string attributeCode);
		Task<ProductAttribute> Update(string attributeCode, ProductAttribute attribute);
		Task DeleteProductAttributeOption(string attributeCode, string optionValue);
		Task<ProductAttribute> GetById(long id);
	}
}