using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IProductMediaRepository
	{
		Task Create(string sku, ProductMedia entry);
		Task<List<ProductMedia>> GetForSku(string sku);
		Task Delete(string sku, int entryId);
		Task<ProductMedia> Get(string sku, int entryId);
		Task<ProductMedia> Update(string sku, int entryId, ProductMedia entry);
	}
}