namespace Magento.RestClient.Domain.Abstractions
{
	public interface IDomainModel
	{
		public bool IsPersisted { get; }


		public void Refresh();
		public void Save();
	}
}