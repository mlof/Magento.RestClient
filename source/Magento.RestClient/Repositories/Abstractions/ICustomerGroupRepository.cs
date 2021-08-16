using Magento.RestClient.Repositories.Abstractions.Customers;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface ICustomerGroupRepository : IReadCustomerGroupRepository, IWriteCustomerGroupRepository
    {
    }
}