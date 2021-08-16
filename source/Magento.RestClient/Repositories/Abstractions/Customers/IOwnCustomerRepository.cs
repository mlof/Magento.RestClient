using Magento.RestClient.Models;
using Magento.RestClient.Models.Customers;

namespace Magento.RestClient.Repositories.Abstractions.Customers
{
    public interface IOwnCustomerRepository
    {
        public Customer GetOwnCustomer();
        public Customer UpdateOwnCustomer(Customer me);
    }
}