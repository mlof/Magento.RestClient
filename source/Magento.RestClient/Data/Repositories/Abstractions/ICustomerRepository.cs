using System.Linq;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Data.Repositories.Abstractions.Customers;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface ICustomerRepository : IReadCustomerRepository, IWriteCustomerRepository, IOwnCustomerRepository, IQueryable<Customer>
	{
		Customer Update(long id, Customer customer);
	}
}