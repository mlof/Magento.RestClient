using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class AttributeSetModelFactory
	{
		private readonly IAdminClient client;

		public AttributeSetModelFactory(IAdminClient client)
		{
			this.client = client;
		}


		public AttributeSetModel GetInstance(string name, EntityType entityType = EntityType.CatalogProduct,
			long? skeletonId = null)
		{

			return new AttributeSetModel(client, name, entityType, skeletonId);
		}

	
	}
}