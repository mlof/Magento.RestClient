using Magento.RestClient.Data.Models.Customers;

namespace Magento.RestClient.Data.Repositories.Abstractions.Customers
{
	public interface IWriteCustomerRepository
	{
		Customer Create(Customer customer, string password = null);
		void DeleteById(long id);
	}
}