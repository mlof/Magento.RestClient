using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Models.Customers;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class CustomerModel : IDomainModel
	{
		private readonly IAdminClient _client;

		public CustomerModel(IAdminClient client, string emailAddress)
		{
			_client = client;
			this.EmailAddress = emailAddress;
		}

		public string EmailAddress { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public long Id { get; private set; }

		public bool IsPersisted { get; private set; }

		public void Refresh()
		{
			var existing = _client.Customers.GetByEmailAddress(this.EmailAddress);
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
				_client.Customers.Update(this.Id, customer);
			}
			else
			{
				_client.Customers.Create(customer);
			}

			Refresh();
		}
	}
}