using MagentoApi.Abstractions;
using MagentoApi.Models;
using MagentoApi.Repositories.Abstractions;
using RestSharp;

namespace MagentoApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(IRestClient client)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetById { get; }
        public ValidationResult Validate(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public Address GetBillingAddress(long customerId)
        {
            throw new System.NotImplementedException();
        }

        public Address GetShippingAddress(long customerId)
        {
            throw new System.NotImplementedException();
        }

        public Customer Create(Customer customer, string password)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(long id)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetOwnCustomer()
        {
            throw new System.NotImplementedException();
        }

        public Customer UpdateOwnCustomer(Customer me)
        {
            throw new System.NotImplementedException();
        }
    }
}