using MagentoApi.Repositories.Abstractions.Customers;

namespace MagentoApi.Repositories.Abstractions
{
    public interface ICustomerRepository : IReadCustomerRepository, IWriteCustomerRepository, IOwnCustomerRepository
    {
    }
}