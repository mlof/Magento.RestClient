using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Exceptions;

namespace Magento.RestClient.Abstractions
{
	/// <summary>
	///     Gets installed modules.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="MagentoException"></exception>
	public interface ICanGetModules
	{
		Task<List<string>> GetModules();
	}
}