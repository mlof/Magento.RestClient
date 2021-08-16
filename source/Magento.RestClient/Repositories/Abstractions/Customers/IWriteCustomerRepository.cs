using Magento.RestClient.Models;
using Magento.RestClient.Models.Customers;

namespace Magento.RestClient.Repositories.Abstractions.Customers
{
    public interface IWriteCustomerRepository
    {
        Customer Create(Customer customer, string password);
        void DeleteById(long id);

    }
}