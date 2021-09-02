using System;
using Magento.RestClient.Models.Common;

namespace Magento.RestClient.Models.Attributes
{
	public static class EntityTypeExtensions
	{
		public static string ToTypeCode(this EntityType type)
		{
			switch (type)
			{
				case EntityType.Customer: return "customer";
				case EntityType.CustomerAddress: return "customer_address";
				case EntityType.CatalogCategory: return "catalog_category";
				case EntityType.CatalogProduct: return "catalog_product";
				case EntityType.Order: return "order";
				case EntityType.Invoice: return "invoice";
				case EntityType.Creditmemo: return "creditmemo";
				case EntityType.Shipment: return "shipment";
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}