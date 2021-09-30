using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Domain.Models;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ISpecialPriceRepository
	{
		Task<List<SpecialPriceResponse>> AddOrUpdateSpecialPrices(params SpecialPrice[] specialPrices);
		Task DeleteSpecialPrices(params SpecialPrice[] specialPrice);
		Task<List<SpecialPrice>> GetSpecialPrices(params string[] skus);
	}
}