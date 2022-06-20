using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Customers.Abstractions;

namespace Magento.RestClient.Modules.Customers
{
	public class CustomerGroupRepository : AbstractRepository, ICustomerGroupRepository
	{
		public CustomerGroupRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}
	}
}