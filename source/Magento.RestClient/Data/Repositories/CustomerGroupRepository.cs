using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;

namespace Magento.RestClient.Data.Repositories
{
	public class CustomerGroupRepository : AbstractRepository, ICustomerGroupRepository
	{
		public CustomerGroupRepository(IContext context) : base(context)
		{
		}
	}
}