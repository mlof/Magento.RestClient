using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Catalog.Products;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magento.RestClient.Data.Repositories
{
    internal class SpecialPriceRepository : AbstractRepository, ISpecialPriceRepository
    {
        public SpecialPriceRepository(IContext context) : base(context)
        {
        }

        public Task<List<SpecialPriceResponse>> AddOrUpdateSpecialPrices(params SpecialPrice[] specialPrices)
        {
            var request = new RestRequest("products/special-price", Method.Post);

            request.AddJsonBody(new { prices = specialPrices });
            return ExecuteAsync<List<SpecialPriceResponse>>(request);
        }

        public async Task DeleteSpecialPrices(params SpecialPrice[] specialPrice)
        {
            var request = new RestRequest("products/special-price-delete", Method.Post);



            await this.ExecuteAsync(request);
        }

        public Task<List<SpecialPrice>> GetSpecialPrices(params string[] skus)
        {
            var request = new RestRequest("products/special-price-information", Method.Post);

            request.AddJsonBody(new { skus });

            return ExecuteAsync<List<SpecialPrice>>(request);
        }
    }
}