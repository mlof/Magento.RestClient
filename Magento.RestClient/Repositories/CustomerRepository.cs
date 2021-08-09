using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Repositories.Abstractions.Customers;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRestClient _client;

        public CustomerRepository(IRestClient client)
        {
            this._client = client;
        }

        Customer IReadCustomerRepository.GetById(long customerId)
        {
            throw new System.NotImplementedException();
        }

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