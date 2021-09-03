using Magento.RestClient.Data.Repositories.Abstractions.Customers;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ICustomerGroupRepository : IReadCustomerGroupRepository, IWriteCustomerGroupRepository
	{
	}
}