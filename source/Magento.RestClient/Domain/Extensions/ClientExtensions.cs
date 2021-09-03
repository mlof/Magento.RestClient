using Magento.RestClient.Domain.Models;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Extensions
{
	public static class ClientExtensions
	{
		public static AttributeSetModel GetAttributeSetModel(this IAdminContext context, string name,
			EntityType entityType = EntityType.CatalogProduct,
			long? skeletonId = null)
		{
			return new(context, name, entityType, skeletonId);
		}

		public static AttributeModel GetAttributeModel(this IAdminContext context, string attributeCode)
		{
			return new(context, attributeCode);
		}

		public static ProductModel GetProductModel(this IAdminContext context, string sku)
		{
			return new(context, sku);
		}


		public static CartModel CreateNewCartModel(this IAdminContext context)
		{
			return new(context);
		}

		public static CartModel GetExistingCartModel(this IAdminContext context, long id)
		{
			return new(context, id);
		}
	}
}