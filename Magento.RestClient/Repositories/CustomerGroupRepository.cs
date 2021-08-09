using MagentoApi.Abstractions;
using MagentoApi.Repositories.Abstractions;
using RestSharp;

namespace MagentoApi.Repositories
{
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        public CustomerGroupRepository(IRestClient client)
        {
            throw new System.NotImplementedException();
        }
    }
}