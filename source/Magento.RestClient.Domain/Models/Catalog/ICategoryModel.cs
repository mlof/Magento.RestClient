using Magento.RestClient.Abstractions.Domain;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public interface ICategoryModel : IDomainModel
	{

		string Name { get; }
		long? ParentId { get; }

		void SetParentId(long id);

		public ICategoryModel GetOrCreateChild(string name);
		void AddProduct(string productSku);
	}
}