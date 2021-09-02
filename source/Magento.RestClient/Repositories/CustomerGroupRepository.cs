using Magento.RestClient.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Repositories
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