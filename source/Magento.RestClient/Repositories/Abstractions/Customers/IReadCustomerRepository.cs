using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Customers;

namespace Magento.RestClient.Repositories.Abstractions.Customers
{
	public interface IReadCustomerRepository
	{
		Customer GetByEmailAddress(string emailAddress);

		Customer GetById(long customerId);
		ValidationResult Validate(Customer customer);
		Address GetBillingAddress(long customerId);
		Address GetShippingAddress(long customerId);
	}
}