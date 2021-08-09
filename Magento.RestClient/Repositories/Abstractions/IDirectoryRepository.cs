using System.Collections.Generic;
using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IDirectoryRepository
    {
        public List<Country> GetCountries();
        public List<Currency> GetCurrencies();
        public Country GetCountry(string countryId);
    }
}