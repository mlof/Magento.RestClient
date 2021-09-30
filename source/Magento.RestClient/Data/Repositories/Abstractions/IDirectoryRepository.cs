using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IDirectoryRepository
	{
		public Task<List<Country>> GetCountries();
		public Task<List<Currency>> GetCurrencies();
		public Task<Country> GetCountry(string countryId);
	}
}