using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class CustomerModel : IDomainModel
	{
		private readonly IAdminContext _context;

		public CustomerModel(IAdminContext context, string emailAddress)
		{
			_context = context;
			this.EmailAddress = emailAddress;
		}

		public string EmailAddress { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public long Id { get; private set; }

		public bool IsPersisted { get; private set; }

		public void Refresh()
		{
			var existing = _context.Customers.GetByEmailAddress(this.EmailAddress);
			if (existing != null)
			{
				this.IsPersisted = true;
				this.Id = existing.Id;
				this.EmailAddress = existing.Email;
				this.FirstName = existing.Firstname;
				this.LastName = existing.Lastname;
			}
			else
			{
				this.IsPersisted = false;
			}
		}

		public void Save()
		{
			var customer = new Customer {
				Firstname = this.FirstName, Lastname = this.LastName, Email = this.EmailAddress
			};
			if (this.IsPersisted)
			{
				_context.Customers.Update(this.Id, customer);
			}
			else
			{
				_context.Customers.Create(customer);
			}

			Refresh();
		}

		public void Delete()
		{
			_context.Customers.DeleteById(Id);
		}
	}
}