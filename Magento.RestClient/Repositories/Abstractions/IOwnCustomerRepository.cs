using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IOwnCustomerRepository
    {
        public Customer GetOwnCustomer();
        public Customer UpdateOwnCustomer(Customer me);
    }
}