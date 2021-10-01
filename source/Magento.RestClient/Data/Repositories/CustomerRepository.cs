using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Data.Repositories.Abstractions;
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
			var customer = this.AsQueryable().SingleOrDefault(customer => customer.Email == emailAddress);

			if (customer == null)
			{
				Log.Warning("Customer by {emailAddress} was not found.", emailAddress);
				return null;
			}

			return customer;
		}

		public async Task<Customer> GetById(long customerId)
		{
			var request = new RestRequest("customers/{id}");

			request.Method = Method.GET;

			request.AddOrUpdateParameter("id", customerId, ParameterType.UrlSegment);
			var response = await Client.ExecuteAsync<Customer>(request);
			return HandleResponse(response);
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
			await _customerValidator.ValidateAndThrowAsync(customer);
			var request = new RestRequest("customers");
			request.Method = Method.POST;
			request.AddJsonBody(new {customer, password});
			var response = await Client.ExecuteAsync<Customer>(request);

			return HandleResponse(response);
		}

		public async Task DeleteById(long id)
		{
			var request = new RestRequest("customers/{id}");

			request.Method = Method.DELETE;

			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			await Client.ExecuteAsync(request);
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
			_customerValidator.ValidateAndThrow(customer);
			var request = new RestRequest("customers/{id}");

			request.Method = Method.PUT;
			request.AddJsonBody(new {customer});
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);

			var response = await Client.ExecuteAsync<Customer>(request);

			return HandleResponse(response);
		}


		public IQueryable<Customer> AsQueryable()
		{
			return new MagentoQueryable<Customer>(Client, "customers/search");
		}
	}
}