using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magento.RestClient.Modules.System.Abstractions
{
	public interface IModuleRepository
	{
		Task<List<string>> GetModules();
	}
}