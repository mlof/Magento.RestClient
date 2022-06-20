using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Customers.Models;
using Magento.RestClient.Modules.Directory.Abstractions;
using Magento.RestClient.Modules.Directory.Models;

namespace Magento.RestClient.Modules.Directory
{
	public class DirectoryRepository : AbstractRepository, IDirectoryRepository
	{
		public DirectoryRepository(IMagentoContext magentoContext) : base(magentoContext)
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