using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IRestClient _client;

        public InvoiceRepository(IRestClient client)
        {
            this._client = client;
        }
    }
}