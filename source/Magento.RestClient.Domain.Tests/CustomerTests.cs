using FluentAssertions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class CustomerTests : AbstractDomainObjectTest
	{
		[Test]
		public void CreateCustomer()
		{
			var customer = new CustomerModel(this.Context, "user@example.org");
			customer.FirstName = "First Name";
			customer.LastName = "Last Name";

			customer.IsPersisted.Should().BeFalse();

			customer.Save();
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