using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Exceptions.Generic;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class ModuleRepository : AbstractRepository, IModuleRepository
	{
		public ModuleRepository(IContext context) : base(context)
		{
		}

		public async Task<List<string>> GetModules()
		{
			var request = new RestRequest("modules");

			var response = await this.Client.ExecuteAsync<List<string>>(request).ConfigureAwait(false);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}
	}
}