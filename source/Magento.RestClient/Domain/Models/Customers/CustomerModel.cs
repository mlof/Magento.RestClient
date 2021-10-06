using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Customers;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models.Customers
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

		public async Task Refresh()
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

		public async Task SaveAsync()
		{
			var customer = new Customer {
				Firstname = this.FirstName, Lastname = this.LastName, Email = this.EmailAddress
			};
			if (this.IsPersisted)
			{
				await _context.Customers.Update(this.Id, customer).ConfigureAwait(false);
			}
			else
			{
				await _context.Customers.Create(customer).ConfigureAwait(false);
			}

			await Refresh().ConfigureAwait(false);
		}

		public async Task Delete()
		{
			await _context.Customers.DeleteById(this.Id).ConfigureAwait(false);
		}
	}
}