using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Modules.Catalog.Models.Products;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface ISpecialPriceRepository
	{
		Task<List<SpecialPriceResponse>> AddOrUpdateSpecialPrices(params SpecialPrice[] specialPrices);
		Task DeleteSpecialPrices(params SpecialPrice[] specialPrice);
		Task<List<SpecialPrice>> GetSpecialPrices(params string[] skus);
	}
}