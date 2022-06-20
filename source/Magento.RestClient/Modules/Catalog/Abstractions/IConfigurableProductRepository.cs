using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Modules.AsynchronousOperations.Models;
using Magento.RestClient.Modules.Catalog.Models.Products;
using Magento.RestClient.Requests;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IConfigurableProductRepository
	{
		public Task CreateChild(string parentSku, string childSku);
		public Task DeleteChild(string parentSku, string childSku);
		public Task<List<Product>> GetConfigurableChildren(string parentSku);

		Task CreateOption(string parentSku, ConfigurableProductOption option);
		Task<List<ConfigurableProductOption>> GetOptions(string parentSku);
		Task UpdateOption(string parentSku, long optionId, ConfigurableProductOption option);

		public Task<BulkActionResponse> BulkMergeConfigurableOptions(
			params ConfigurableProductOptionRequest[] requests);

		Task<BulkActionResponse> BulkMergeConfigurations(params
			CreateOrUpdateConfigurationRequest[] configurations);
	}
}