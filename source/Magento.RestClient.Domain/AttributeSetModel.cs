using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search.Abstractions;

namespace Magento.RestClient.Domain
{
	public class AttributeSetModel : IDomainModel
	{
		private List<AttributeGroup> _attributeGroups;
		private List<EntityAttribute> _attributes;
		private readonly long _skeletonId;
		private readonly IAdminClient _client;
		private AttributeSet _model;

		private List<AttributeAssignment> _attributeAssignments = new List<AttributeAssignment>();

		public AttributeSetModel(IAdminClient adminClient, string name, EntityType entityType,
			long? skeletonId)
		{
			_client = adminClient;

			this.EntityType = entityType;
			if (entityType != EntityType.CatalogProduct)
			{
				throw new NotImplementedException();
			}

			var searchResponse = _client.Search.AttributeSets(builder =>
				builder.WhereEquals(set => set.AttributeSetName, name)
					.WhereEquals(set => set.EntityTypeId, entityType));
			if (searchResponse.TotalCount == 1)
			{
				var r = searchResponse.Items.Single();
				var id = r.AttributeSetId.Value;
				_model = _client.AttributeSets.Get(id);

				Refresh();
			}
			else
			{
				_model = new AttributeSet {AttributeSetName = name, EntityTypeId = entityType};
				_attributes = new List<EntityAttribute>();
				_attributeGroups = new List<AttributeGroup>();
				if (skeletonId != null)
				{
					_skeletonId = skeletonId.Value;
				}
				else
				{
					var attributeSetId = _client.Search.GetDefaultAttributeSet(this.EntityType).AttributeSetId;
					if (attributeSetId != null)
					{
						_skeletonId = attributeSetId.Value;
					}
				}
			}
		}

		private EntityType EntityType { get; }

		public IReadOnlyList<EntityAttribute> Attributes {
			get => _attributes;
		}

		public IReadOnlyList<AttributeGroup> AttributeGroups {
			get => _attributeGroups;
		}

		public long Id {
			get => _model.AttributeSetId.GetValueOrDefault();
		}


		public void Refresh()
		{
			_model = _client.AttributeSets.Get(Id);
			_attributes = _client.Attributes.GetProductAttributes(Id).ToList();
			var searchResponse = _client.Search.ProductAttributeGroups(builder =>
				builder.WhereEquals(group => group.AttributeSetId, Id)
					.WithPageSize(0));
			_attributeGroups = searchResponse.Items.ToList();
		}

		public void Save()
		{
			if (_model.AttributeSetId == null)
			{
				_model = _client.AttributeSets.Create(this.EntityType, _skeletonId, _model);
			}

			foreach (var attributeGroup in _attributeGroups)
			{
				if (attributeGroup.AttributeGroupId == 0)
				{
					attributeGroup.AttributeGroupId = _client.AttributeSets.CreateProductAttributeGroup(this.Id,
						attributeGroup.AttributeGroupName);

				}

			}

			foreach (var assignment in _attributeAssignments)
			{
				var groupId = _attributeGroups.Single(group => group.AttributeGroupName == assignment.GroupName);
				_client.AttributeSets.AssignProductAttribute(Id, groupId.AttributeGroupId, assignment.AttributeCode);
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
			else
			{
				_attributeAssignments.Add(new AttributeAssignment() {
					AttributeCode = attributeCode, GroupName = attributeGroup
				});
			}
		}
	}

	public class AttributeAssignment
	{
		public string AttributeCode { get; set; }
		public string GroupName { get; set; }
	}

	public static class SearchExtensions
	{
		public static AttributeSet GetDefaultAttributeSet(this ISearchService search, EntityType entityType)
		{
			var response = search.AttributeSets(builder =>
				builder.WhereEquals(set => set.AttributeSetName, "Default")
					.WhereEquals(set => set.EntityTypeId, entityType)
					.WithPageSize(0));
			return response.Items.Single();
		}
	}
}