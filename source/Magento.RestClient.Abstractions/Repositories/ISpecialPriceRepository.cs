using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface ISpecialPriceRepository
	{
		Task<List<SpecialPriceResponse>> AddOrUpdateSpecialPrices(params SpecialPrice[] specialPrices);
		Task DeleteSpecialPrices(params SpecialPrice[] specialPrice);
		Task<List<SpecialPrice>> GetSpecialPrices(params string[] skus);
	}
}