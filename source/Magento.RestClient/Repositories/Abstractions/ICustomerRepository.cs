using Magento.RestClient.Models.Customers;
using Magento.RestClient.Repositories.Abstractions.Customers;

namespace Magento.RestClient.Repositories.Abstractions
{
	public interface ICustomerRepository : IReadCustomerRepository, IWriteCustomerRepository, IOwnCustomerRepository
	{
		Customer Update(long id, Customer customer);
	}
}