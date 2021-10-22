using Magento.RestClient.Abstractions.Repositories.Customers;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface ICustomerGroupRepository : IReadCustomerGroupRepository, IWriteCustomerGroupRepository
	{
	}
}