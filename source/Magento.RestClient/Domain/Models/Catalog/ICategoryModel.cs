using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public interface ICategoryModel : IDomainModel
	{

		string Name { get; }
		long? ParentId { get; }

		void SetParentId(long id);

		public ICategoryModel GetOrCreateChild(string name);
	}
}