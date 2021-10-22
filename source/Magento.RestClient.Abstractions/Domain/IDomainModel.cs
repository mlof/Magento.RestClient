using System.Threading.Tasks;

namespace Magento.RestClient.Abstractions.Domain
{
	public interface IDomainModel
	{
		public bool IsPersisted { get; }

		public Task Refresh();
		public Task SaveAsync();
		public Task Delete();
	}
}