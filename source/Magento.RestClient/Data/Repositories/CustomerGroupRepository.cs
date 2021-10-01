using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Repositories.Abstractions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class CustomerGroupRepository : AbstractRepository, ICustomerGroupRepository
	{

		public CustomerGroupRepository(IContext context) : base(context)
		{
		}
	}
}