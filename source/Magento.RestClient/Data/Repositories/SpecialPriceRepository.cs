using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Models;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class SpecialPriceRepository : AbstractRepository, ISpecialPriceRepository
	{
		private readonly IRestClient _client;

		public SpecialPriceRepository(IRestClient client)
		{
			this._client = client;
		}

		public async Task<List<SpecialPriceResponse>> AddOrUpdateSpecialPrices(params SpecialPrice[] specialPrices)
		{
			var request = new RestRequest("products/special-price");

			request.Method = Method.POST;
			request.AddJsonBody(new {prices = specialPrices});
			var response = await _client.ExecuteAsync<List<SpecialPriceResponse>>(request);


			return HandleResponse(response);

			throw new System.NotImplementedException();
		}

		public async Task DeleteSpecialPrices(params SpecialPrice[] specialPrice)
		{
			var request = new RestRequest("products/special-price-delete");
		}

		public async Task<List<SpecialPrice>> GetSpecialPrices(params string[] skus)
		{
			var request = new RestRequest("products/special-price-information");

			request.Method = Method.POST;
			request.AddJsonBody(new {skus});

			var response = await _client.ExecuteAsync<List<SpecialPrice>>(request);
			return HandleResponse(response);
		}
	}
}