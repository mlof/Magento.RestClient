using System.Collections.Generic;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IDirectoryRepository
	{
		public List<Country> GetCountries();
		public List<Currency> GetCurrencies();
		public Country GetCountry(string countryId);
	}
}