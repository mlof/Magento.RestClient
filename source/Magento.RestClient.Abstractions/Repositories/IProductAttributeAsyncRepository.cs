using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Context
{
	public interface IProductAttributeAsyncRepository
	{
		Task<BulkActionResponse> PutAttributesByAttributeCode(params ProductAttribute[] attributes);
	}
}