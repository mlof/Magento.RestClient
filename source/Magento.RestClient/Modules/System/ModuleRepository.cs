using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Exceptions.Generic;
using Magento.RestClient.Modules.System.Abstractions;
using RestSharp;

namespace Magento.RestClient.Modules.System
{
	public class ModuleRepository : AbstractRepository, IModuleRepository
	{
		public ModuleRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public async  Task<List<string>> GetModules()
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