using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions.Generic;
using RestSharp;

namespace Magento.RestClient.Abstractions
{
	public class ModuleRepository : AbstractRepository, IModuleRepository
	{
		public ModuleRepository(IContext context) : base(context)
		{
		}

		/// <inheritdoc cref="ICanGetModules" />
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