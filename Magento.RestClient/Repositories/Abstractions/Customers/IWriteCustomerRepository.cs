using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions.Customers
{
    public interface IWriteCustomerRepository
    {
        Customer Create(Customer customer, string password);
        void DeleteById(long id);

    }
}