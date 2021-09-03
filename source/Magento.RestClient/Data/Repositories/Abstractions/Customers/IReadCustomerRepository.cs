using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;

namespace Magento.RestClient.Data.Repositories.Abstractions.Customers
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