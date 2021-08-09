using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions.Customers
{
    public interface IWriteCustomerRepository
    {
        Customer Create(Customer customer, string password);
        void DeleteById(long id);

    }
}