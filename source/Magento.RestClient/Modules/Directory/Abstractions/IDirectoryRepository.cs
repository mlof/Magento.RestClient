using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Modules.Customers.Models;
using Magento.RestClient.Modules.Directory.Models;

namespace Magento.RestClient.Modules.Directory.Abstractions
{
	public interface IDirectoryRepository
	{
		public Task<List<Country>> GetCountries();
		public Task<List<Currency>> GetCurrencies();
		public Task<Country> GetCountry(string countryId);
	}
}