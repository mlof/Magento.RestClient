using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IModuleRepository
	{
		Task<List<string>> GetModules();
	}
}