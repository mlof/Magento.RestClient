using FluentValidation;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Repositories.Abstractions.Customers;
using Magento.RestClient.Validators;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRestClient _client;
        private readonly CustomerValidator _customerValidator;

        public CustomerRepository(IRestClient client)
        {
            this._client = client;
            this._customerValidator = new CustomerValidator();
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
            _customerValidator.ValidateAndThrow(customer);
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
            _customerValidator.ValidateAndThrow(me);

            throw new System.NotImplementedException();
        }
    }
}