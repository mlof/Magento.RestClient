using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Common;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class DirectoryRepository : AbstractRepository, IDirectoryRepository
	{
		public DirectoryRepository(IContext context) : base(context)
		{
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