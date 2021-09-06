using System.Collections.Generic;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IAttributeRepository
	{
		IEnumerable<EntityAttribute> GetProductAttributes(long attributeSetId);

		ProductAttribute Create(ProductAttribute attribute);
		void DeleteProductAttribute(string attributeCode);
		List<Option> GetProductAttributeOptions(string attributeCode);
		int CreateProductAttributeOption(string attributeCode, Option option);
		ProductAttribute GetByCode(string code);
		ProductAttribute Update(string attributeCode, ProductAttribute attribute);
		void DeleteProductAttributeOption(string attributeCode, string optionValue);
		ProductAttribute GetById(long id);
	}
}