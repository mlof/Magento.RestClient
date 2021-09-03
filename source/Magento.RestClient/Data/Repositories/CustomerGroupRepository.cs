using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class CustomerGroupRepository : ICustomerGroupRepository
	{
		private readonly IRestClient _client;

		public CustomerGroupRepository(IRestClient client)
		{
			_client = client;
		}
	}
}