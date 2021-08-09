using System.Collections.Generic;
using MagentoApi.Abstractions;
using MagentoApi.Models;
using MagentoApi.Repositories.Abstractions;
using RestSharp;

namespace MagentoApi.Repositories
{
    public class DirectoryRepository : IDirectoryRepository
    {
        public DirectoryRepository(IRestClient client)
        {
            throw new System.NotImplementedException();
        }

        public List<Country> GetCountries()
        {
            throw new System.NotImplementedException();
        }

        public List<Currency> GetCurrencies()
        {
            throw new System.NotImplementedException();
        }

        public Country GetCountry(string countryId)
        {
            throw new System.NotImplementedException();
        }
    }
}