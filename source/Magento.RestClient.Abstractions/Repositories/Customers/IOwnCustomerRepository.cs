using Magento.RestClient.Data.Models.Customers;

namespace Magento.RestClient.Abstractions.Repositories.Customers
{
	public interface IOwnCustomerRepository
	{
		public Customer GetOwnCustomer();
		public Customer UpdateOwnCustomer(Customer me);
	}
}