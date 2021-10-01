using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IProductMediaRepository
	{
		Task<ProductMedia> Create(string sku, ProductMedia entry);
		Task<List<ProductMedia>> GetForSku(string sku);
		Task<bool> Delete(string sku, long entryId);
		Task<ProductMedia> Get(string sku, int entryId);
		Task<ProductMedia> Update(string sku, long entryId, ProductMedia entry);
	}
}