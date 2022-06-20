using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.Common.Models;
using Magento.RestClient.Modules.Customers.Models;

namespace Magento.RestClient.Modules.Customers.Abstractions
{
	public interface ICustomerRepository :  IHasQueryable<Customer>
	{
		Customer GetByEmailAddress(string emailAddress);
		Task<Customer> Create(Customer customer, string password = null);
		Task DeleteById(long id);
		Task<Customer> GetById(long customerId);
		Task<ValidationResult> Validate(Customer customer);
		Task<Address> GetBillingAddress(long customerId);
		Task<Address> GetShippingAddress(long customerId);
		Task<Customer> Update(long id, Customer customer);
		Customer GetOwnCustomer();
		Customer UpdateOwnCustomer(Customer me);
	}
}