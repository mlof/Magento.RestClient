using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions.Customers
{
    public interface IOwnCustomerRepository
    {
        public Customer GetOwnCustomer();
        public Customer UpdateOwnCustomer(Customer me);
    }
}