using Magento.RestClient.Domain.Models;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Extensions
{
	public static class ClientExtensions
	{
		public static AttributeSetModel GetAttributeSetModel(this IAdminClient client, string name,
			EntityType entityType = EntityType.CatalogProduct,
			long? skeletonId = null)
		{
			return new(client, name, entityType, skeletonId);
		}

		public static AttributeModel GetAttributeModel(this IAdminClient client, string attributeCode)
		{
			return new(client, attributeCode);
		}

		public static ProductModel GetProductModel(this IAdminClient client, string sku)
		{
			return new(client, sku);
		}


		public static CartModel CreateNewCartModel(this IAdminClient client)
		{
			return new(client);
		}

		public static CartModel GetExistingCartModel(this IAdminClient client, long id)
		{
			return new(client, id);
		}
	}
}