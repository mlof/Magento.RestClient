using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface IOwnCustomerRepository
    {
        public Customer GetOwnCustomer();
        public Customer UpdateOwnCustomer(Customer me);
    }
}