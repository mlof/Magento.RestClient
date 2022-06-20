using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.Catalog.Models.Products;
using RestSharp;

namespace Magento.RestClient.Modules.Catalog
{
	internal class SpecialPriceRepository : AbstractRepository, ISpecialPriceRepository
	{
		public SpecialPriceRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public Task<List<SpecialPriceResponse>> AddOrUpdateSpecialPrices(params SpecialPrice[] specialPrices)
		{
			var request = new RestRequest("products/special-price", Method.Post);
			
			request.AddJsonBody(new {prices = specialPrices});
			return ExecuteAsync<List<SpecialPriceResponse>>(request);
		}

		public async  Task DeleteSpecialPrices(params SpecialPrice[] specialPrice)
		{
			var request = new RestRequest("products/special-price-delete", Method.Post);


			await ExecuteAsync(request);
		}

		public Task<List<SpecialPrice>> GetSpecialPrices(params string[] skus)
		{
			var request = new RestRequest("products/special-price-information", Method.Post);

			request.AddJsonBody(new {skus});

			return ExecuteAsync<List<SpecialPrice>>(request);
		}
	}
}