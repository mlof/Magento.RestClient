using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magento.RestClient.Abstractions
{
	public interface IModuleRepository
	{
		Task<List<string>> GetModules();
	}
}