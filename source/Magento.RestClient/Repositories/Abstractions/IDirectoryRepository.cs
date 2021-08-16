using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Common;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IDirectoryRepository
    {
        public List<Country> GetCountries();
        public List<Currency> GetCurrencies();
        public Country GetCountry(string countryId);
    }
}