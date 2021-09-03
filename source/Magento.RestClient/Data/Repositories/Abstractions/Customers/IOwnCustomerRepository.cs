using Magento.RestClient.Data.Models.Customers;

namespace Magento.RestClient.Data.Repositories.Abstractions.Customers
{
	public interface IOwnCustomerRepository
	{
		public Customer GetOwnCustomer();
		public Customer UpdateOwnCustomer(Customer me);
	}
}