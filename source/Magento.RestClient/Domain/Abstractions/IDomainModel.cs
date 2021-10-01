using System.Threading.Tasks;
using FluentValidation;

namespace Magento.RestClient.Domain.Abstractions
{
	public interface IDomainModel
	{
		public bool IsPersisted { get; }


		public Task Refresh();
		public Task SaveAsync();
		public Task Delete();
	}
}