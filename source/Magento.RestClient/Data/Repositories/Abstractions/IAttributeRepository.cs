using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Models.EAV.Attributes;

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
		/// <summary>
		/// Updates an attribute by its code. 
		/// </summary>
		/// <remarks>Magento breaks on this as of 2.4.2</remarks>
		/// <param name="attributeCode"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>

		Task<ProductAttribute> Update(string attributeCode, ProductAttribute attribute);
		Task DeleteProductAttributeOption(string attributeCode, string optionValue);
		Task<ProductAttribute> GetById(long id);
	}
}