using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IConfigurableProductRepository
	{
		public Task CreateChild(string parentSku, string childSku);
		public Task DeleteChild(string parentSku, string childSku);
		public Task<List<Product>> GetConfigurableChildren(string parentSku);


		Task CreateOption(string parentSku, ConfigurableProductOption option);
		Task<List<ConfigurableProductOption>> GetOptions(string parentSku);
		Task UpdateOption(string parentSku, long optionId, ConfigurableProductOption option);
	}
}