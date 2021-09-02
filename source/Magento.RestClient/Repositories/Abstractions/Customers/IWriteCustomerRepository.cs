using Magento.RestClient.Models.Customers;

namespace Magento.RestClient.Repositories.Abstractions.Customers
{
	public interface IWriteCustomerRepository
	{
		Customer Create(Customer customer, string password = null);
		void DeleteById(long id);
	}
}