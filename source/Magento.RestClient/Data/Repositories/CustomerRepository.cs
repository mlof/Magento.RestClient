using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
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
		private readonly IRestClient _client;
		private readonly CustomerValidator _customerValidator;
		private IQueryable<Customer> _customerRepositoryImplementation => new MagentoQueryable<Customer>(_client, "customers/search");

		public CustomerRepository(IRestClient client)
		{
			_client = client;
			_customerValidator = new CustomerValidator();
		}

		public Customer GetByEmailAddress(string emailAddress)
		{
			var customer = this.SingleOrDefault(customer => customer.Email == emailAddress);

			if (customer == null)
			{
				Log.Warning("Customer by {emailAddress} was not found.", emailAddress);
				return null;
			}

			return customer;
		}

		public Customer GetById(long customerId)
		{
			var request = new RestRequest("customers/{id}");

			request.Method = Method.GET;

			request.AddOrUpdateParameter("id", customerId, ParameterType.UrlSegment);
			var response = _client.Execute<Customer>(request);
			return HandleResponse(response);
		}

		public ValidationResult Validate(Customer customer)
		{
			throw new NotImplementedException();
		}

		public Address GetBillingAddress(long customerId)
		{
			throw new NotImplementedException();
		}

		public Address GetShippingAddress(long customerId)
		{
			throw new NotImplementedException();
		}

		public Customer Create(Customer customer, string password)
		{
			_customerValidator.ValidateAndThrow(customer);
			var request = new RestRequest("customers");
			request.Method = Method.POST;
			request.AddJsonBody(new {customer, password});
			var response = _client.Execute<Customer>(request);

			return HandleResponse(response);
		}

		public void DeleteById(long id)
		{
			var request = new RestRequest("customers/{id}");

			request.Method = Method.DELETE;

			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			_client.Execute(request);
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

		public Customer Update(long id, Customer customer)
		{
			_customerValidator.ValidateAndThrow(customer);
			var request = new RestRequest("customers/{id}");

			request.Method = Method.PUT;
			request.AddJsonBody(new {customer});
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);

			var response = _client.Execute<Customer>(request);

			return HandleResponse(response);
		}

		public IEnumerator<Customer> GetEnumerator()
		{
			return _customerRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _customerRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => _customerRepositoryImplementation.ElementType;

		public Expression Expression => _customerRepositoryImplementation.Expression;

		public IQueryProvider Provider => _customerRepositoryImplementation.Provider;
	}
}