using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Domain.Models.Customers;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class CustomerTests : AbstractAdminTest
	{
		[Test]
		async public Task CreateCustomer()
		{
			var customer = new CustomerModel(this.Context, "user@example.org");
			customer.FirstName = "First Name";
			customer.LastName = "Last Name";

			customer.IsPersisted.Should().BeFalse();

			await customer.SaveAsync();

			customer.IsPersisted.Should().BeTrue();

			customer.FirstName.Should().BeEquivalentTo("First Name");
		}

		[TearDown]
		public void CustomerTeardown()
		{
			var customer = Context.Customers.GetByEmailAddress("user@example.org");
			Context.Customers.DeleteById(customer.Id);
		}
	}
}