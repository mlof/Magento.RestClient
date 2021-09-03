using System.Collections.Generic;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IProductMediaRepository
	{
		void Create(string sku, ProductMedia entry);
		List<ProductMedia> GetForSku(string sku);
		void Delete(string sku, int entryId);
		ProductMedia Get(string sku, int entryId);
		ProductMedia Update(string sku, int entryId, ProductMedia entry);
	}
}