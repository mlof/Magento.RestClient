using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class DirectoryRepository : IDirectoryRepository
	{
		private readonly IRestClient _client;

		public DirectoryRepository(IRestClient client)
		{
			_client = client;
		}

		public Task<List<Country>> GetCountries()
		{
			throw new NotImplementedException();
		}

		public Task<List<Currency>> GetCurrencies()
		{
			throw new NotImplementedException();
		}

		public Task<Country> GetCountry(string countryId)
		{
			throw new NotImplementedException();
		}
	}
}