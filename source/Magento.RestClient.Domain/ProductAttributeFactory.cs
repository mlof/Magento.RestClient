using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class ProductAttributeFactory
	{
		private readonly IAdminClient client;

		public ProductAttributeFactory(IAdminClient client)
		{
			this.client = client;
		}

		public AttributeModel GetInstance(string attributeCode)
		{

			return new AttributeModel(client, attributeCode);

		}

	}
}