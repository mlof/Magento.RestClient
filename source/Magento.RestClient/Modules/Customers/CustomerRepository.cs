using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Common.Models;
using Magento.RestClient.Modules.Customers.Abstractions;
using Magento.RestClient.Modules.Customers.Models;
using Magento.RestClient.Modules.Customers.Validation;
using RestSharp;
using Serilog;

namespace Magento.RestClient.Modules.Customers
{
	internal class CustomerRepository : AbstractRepository, ICustomerRepository
	{
		private readonly CustomerValidator _customerValidator;

		public CustomerRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
			_customerValidator = new CustomerValidator();
		}

		public Customer GetByEmailAddress(string emailAddress)
		{
			var customer = AsQueryable().SingleOrDefault(customer => customer.Email == emailAddress);

			if (customer == null)
			{
				Log.Warning("Customer by {EmailAddress} was not found.", emailAddress);
				return null;
			}

			return customer;
		}

		public Task<Customer> GetById(long customerId)
		{
			var request = new RestRequest("customers/{id}");

			request.AddOrUpdateParameter("id", customerId, ParameterType.UrlSegment);
			return ExecuteAsync<Customer>(request);
		}

		public Task<ValidationResult> Validate(Customer customer)
		{
			throw new NotImplementedException();
		}

		public Task<Address> GetBillingAddress(long customerId)
		{
			throw new NotImplementedException();
		}

		public Task<Address> GetShippingAddress(long customerId)
		{
			throw new NotImplementedException();
		}

		public async  Task<Customer> Create(Customer customer, string password = null)
		{
			await _customerValidator.ValidateAndThrowAsync(customer).ConfigureAwait(false);
			var request = new RestRequest("customers", Method.Post);
			request.AddJsonBody(new {customer, password});
			return await ExecuteAsync<Customer>(request).ConfigureAwait(false);
		}

		public Task DeleteById(long id)
		{
			var request = new RestRequest("customers/{id}", Method.Delete);

			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			return this.Client.ExecuteAsync(request);
		}

		public Customer GetOwnCustomer()
		{
			throw new NotImplementedException();
		}

		public Customer UpdateOwnCustomer(Customer me)
		{
			_customerValidator.ValidateAndThrow(me);

			throw new NotImplementedException();
		}

		public async  Task<Customer> Update(long id, Customer customer)
		{
			await _customerValidator.ValidateAndThrowAsync(customer).ConfigureAwait(false);
			var request = new RestRequest("customers/{id}", Method.Put);

			request.AddJsonBody(new {customer});
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);

			return await ExecuteAsync<Customer>(request).ConfigureAwait(false);
		}

		public IQueryable<Customer> AsQueryable()
		{
			return new MagentoQueryable<Customer>(this.Client, "customers/search");
		}
	}
}