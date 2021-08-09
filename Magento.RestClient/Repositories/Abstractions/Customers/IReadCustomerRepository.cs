using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions.Customers
{
    public interface IReadCustomerRepository
    {
        Customer GetById { get; }
        ValidationResult Validate(Customer customer);
        Address GetBillingAddress(long customerId);
        Address GetShippingAddress(long customerId);
    }
}