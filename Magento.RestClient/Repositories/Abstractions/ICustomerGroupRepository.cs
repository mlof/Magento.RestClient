using MagentoApi.Repositories.Abstractions.Customers;

namespace MagentoApi.Repositories.Abstractions
{
    public interface ICustomerGroupRepository : IReadCustomerGroupRepository, IWriteCustomerGroupRepository
    {
    }
}