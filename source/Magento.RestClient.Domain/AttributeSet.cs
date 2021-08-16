using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class AttributeSet
	{
		private readonly IIntegrationClient client;
		private long _id;
		private Models.Attributes.AttributeSet _model;
		private IEnumerable<EntityAttribute> _attributes;
		private List<AttributeGroup> _attributeGroups;

		private AttributeSet(IIntegrationClient client)
		{
			this.client = client;
		}


		public static AttributeSet GetExisting(IIntegrationClient client, long attributeSetId)
		{
			var attributeSet = new AttributeSet(client);
			attributeSet.Id = attributeSetId;
			return attributeSet.UpdateMagentoValues();
		}

		public static AttributeSet GetExisting(IIntegrationClient client, string name)
		{
			var attributeSet = new AttributeSet(client);

			attributeSet.Id = client.Search
				.AttributeSets(builder => builder.WhereEquals(set => set.AttributeSetName, name))
				.Items
				.Single()
				.AttributeSetId;

			return attributeSet.UpdateMagentoValues();
		}

		public long Id {
			get { return _id; }
			set {
				_id = value;
				UpdateMagentoValues();
			}
		}

		private AttributeSet UpdateMagentoValues()
		{
			this._model = client.AttributeSets.Get(this.Id);
			if (_model.EntityTypeId == EntityType.CatalogProduct)
			{
				this._attributes = client.Attributes.GetProductAttributes(this.Id);
				this._attributeGroups = client.Search
					.ProductAttributeGroups(builder => builder.WhereEquals(group => group.AttributeSetId, Id)).Items;
			}

			return this;
		}
	}
}