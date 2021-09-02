using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Models.AttributeSets;
using Magento.RestClient.Extensions;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class AttributeSetModel : IDomainModel
	{
		private readonly List<AttributeAssignment> _attributeAssignments = new();
		private readonly IAdminClient _client;
		private List<AttributeGroup> _attributeGroups;
		private List<EntityAttribute> _attributes;
		private long? _skeletonId;

		public AttributeSetModel(IAdminClient adminClient, string name, EntityType entityType,
			long? skeletonId)
		{
			_client = adminClient;

			if (entityType != EntityType.CatalogProduct)
			{
				throw new NotImplementedException();
			}

			this.EntityType = entityType;

			this.Name = name;
			_skeletonId = skeletonId;
			Refresh();
		}

		private EntityType EntityType { get; }

		public IReadOnlyList<EntityAttribute> Attributes => _attributes;

		public IReadOnlyList<AttributeGroup> AttributeGroups => _attributeGroups;

		public long Id { get; set; }

		public string Name {
			get;
			set;
		}

		public bool IsPersisted { get; private set; }


		public void Refresh()
		{
			var searchResponse = _client.Search.AttributeSets(builder =>
				builder.WhereEquals(set => set.AttributeSetName, this.Name)
					.WhereEquals(set => set.EntityTypeId, this.EntityType));
			if (searchResponse.TotalCount == 1)
			{
				this.IsPersisted = true;
				var r = searchResponse.Items.Single();
				this.Id = r.AttributeSetId.Value;

				var _model = _client.AttributeSets.Get(this.Id);

				this.Name = _model.AttributeSetName;

				_attributes = _client.Attributes.GetProductAttributes(this.Id).ToList();
				var attributeGroups = _client.Search.ProductAttributeGroups(builder =>
					builder.WhereEquals(group => group.AttributeSetId, this.Id)
						.WithPageSize(0));
				_attributeGroups = attributeGroups.Items.ToList();
			}
			else
			{
				this.IsPersisted = false;

				_attributes = new List<EntityAttribute>();
				_attributeGroups = new List<AttributeGroup>();
				if (_skeletonId == null)
				{
					var attributeSetId = _client.Search.GetDefaultAttributeSet(this.EntityType).AttributeSetId;
					if (attributeSetId != null)
					{
						_skeletonId = attributeSetId.Value;
					}
				}
			}
		}

		public void Save()
		{
			var attributeSet = new AttributeSet();

			if (this.Id == 0)
			{
				Debug.Assert(_skeletonId != null, nameof(_skeletonId) + " != null");
				_client.AttributeSets.Create(this.EntityType, _skeletonId.Value, attributeSet);
			}

			var currentAttributeGroups = _client.Search.ProductAttributeGroups(builder =>
				builder.WhereEquals(group => group.AttributeSetId, this.Id)
					.WithPageSize(0));


			foreach (var attributeGroup in _attributeGroups)
			{
				if (!currentAttributeGroups.Items.Select(group => group.AttributeGroupName)
					.Contains(attributeGroup.AttributeGroupName))
				{
					attributeGroup.AttributeGroupId = _client.AttributeSets.CreateProductAttributeGroup(this.Id,
						attributeGroup.AttributeGroupName);
				}
				else
				{
					attributeGroup.AttributeGroupId = currentAttributeGroups.Items
						.Where(group => group.AttributeGroupName == attributeGroup.AttributeGroupName)
						.Select(group => group.AttributeGroupId).SingleOrDefault();
				}
			}

			foreach (var assignment in _attributeAssignments)
			{
				var groupId = _attributeGroups.Single(group => group.AttributeGroupName == assignment.GroupName);
				_client.AttributeSets.AssignProductAttribute(this.Id, groupId.AttributeGroupId,
					assignment.AttributeCode);
			}

			_attributeAssignments.Clear();
			Refresh();
		}


		public void AddGroup(string groupName)
		{
			if (!_attributeGroups.Any(group => group.AttributeGroupName == groupName))
			{
				_attributeGroups.Add(new AttributeGroup {AttributeGroupName = groupName});
			}
		}

		public void AssignAttribute(string attributeGroup, string attributeCode)
		{
			if (_attributes.Any(attribute => attribute.AttributeCode == attributeCode))
			{
				throw new InvalidOperationException("Attribute already exists");
			}

			if (!_attributeGroups.Any(group => group.AttributeGroupName == attributeGroup))
			{
				throw new InvalidOperationException("Attribute group does not exist.");
			}

			_attributeAssignments.Add(new AttributeAssignment {
				AttributeCode = attributeCode, GroupName = attributeGroup
			});
		}
	}
}