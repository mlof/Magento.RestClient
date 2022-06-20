using System.Threading.Tasks;
using Magento.RestClient.Modules.AsynchronousOperations.Models;
using Magento.RestClient.Modules.Catalog.Models.Products;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IProductAttributeAsyncRepository
	{
		Task<BulkActionResponse> PutAttributesByAttributeCode(params ProductAttribute[] attributes);
	}
}