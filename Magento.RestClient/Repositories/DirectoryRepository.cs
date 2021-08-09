using System.Collections.Generic;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
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