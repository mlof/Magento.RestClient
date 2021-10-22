using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Expressions;
using Magento.RestClient.Search;
using Magento.RestClient.Validators;
using RestSharp;
using Serilog;

namespace Magento.RestClient.Data.Repositories
{
	internal class CustomerRepository : AbstractRepository, ICustomerRepository
	{
		private readonly CustomerValidator _customerValidator;

		public CustomerRepository(IContext context) : base(context)
		{
			_customerValidator = new CustomerValidator();
		}

		public Customer GetByEmailAddress(string emailAddress)
		{
			var customer = AsQueryable().SingleOrDefault(customer => customer.Email == emailAddress);

			if (customer == null)
			{
				Log.Warning("Customer by {emailAddress} was not found.", emailAddress);
				return null;
			}

			return customer;
		}

		public Task<Customer> GetById(long customerId)
		{
			var request = new RestRequest("customers/{id}", Method.GET);

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

		public async Task<Customer> Create(Customer customer, string password = null)
		{
			await _customerValidator.ValidateAndThrowAsync(customer).ConfigureAwait(false);
			var request = new RestRequest("customers", Method.POST);
			request.AddJsonBody(new {customer, password});
			return await ExecuteAsync<Customer>(request).ConfigureAwait(false);
		}

		public Task DeleteById(long id)
		{
			var request = new RestRequest("customers/{id}", Method.DELETE);

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

		public async Task<Customer> Update(long id, Customer customer)
		{
			await _customerValidator.ValidateAndThrowAsync(customer).ConfigureAwait(false);
			var request = new RestRequest("customers/{id}", Method.PUT);

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