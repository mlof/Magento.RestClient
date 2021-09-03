using System;
using System.Collections.Generic;
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

		public List<Country> GetCountries()
		{
			throw new NotImplementedException();
		}

		public List<Currency> GetCurrencies()
		{
			throw new NotImplementedException();
		}

		public Country GetCountry(string countryId)
		{
			throw new NotImplementedException();
		}
	}
}