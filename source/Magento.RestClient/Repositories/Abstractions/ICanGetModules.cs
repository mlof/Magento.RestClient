using System.Collections.Generic;
using Magento.RestClient.Exceptions;

namespace Magento.RestClient.Repositories.Abstractions
{
	/// <summary>
	/// Gets installed modules.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="MagentoException"></exception>

	public interface ICanGetModules
	{
		List<string> GetModules();

	}
}