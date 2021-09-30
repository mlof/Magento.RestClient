using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Domain.Models.AttributeSets;
using Magento.RestClient.Extensions;

namespace Magento.RestClient.Domain.Models
{
	public class AttributeSetModel : IDomainModel
	{
		private readonly List<AttributeAssignment> _attributeAssignments = new();
		private readonly IAdminContext _context;
		private List<AttributeGroup> _attributeGroups;
		private List<EntityAttribute> _attributes;
		private long? _skeletonId;

		public AttributeSetModel(IAdminContext adminContext, string name, EntityType entityType,
			long? skeletonId = null)
		{
			_context = adminContext;

			if (entityType != EntityType.CatalogProduct)
			{
				throw new NotImplementedException();
			}

			this.EntityType = entityType;

			this.Name = name;
			_skeletonId = skeletonId;
			Refresh().GetAwaiter().GetResult();
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


		public async Task Refresh()
		{
			var searchResponse = _context.AttributeSets.AsQueryable().Where(set =>
				set.AttributeSetName == this.Name && set.EntityTypeId == this.EntityType).ToList();
			if (searchResponse.Count == 1)
			{
				this.IsPersisted = true;
				var r = searchResponse.Single();
				this.Id = r.AttributeSetId.Value;

				var _model = await _context.AttributeSets.Get(this.Id);

				this.Name = _model.AttributeSetName;

				var attributesResponse = await _context.Attributes.GetProductAttributes(this.Id);

				_attributes =
					attributesResponse
						.ToList();
				var attributeGroups =
					_context.ProductAttributeGroups.AsQueryable().Where(group => group.AttributeSetId == Id).ToList();
				_attributeGroups = attributeGroups.ToList();
			}
			else
			{
				this.IsPersisted = false;

				_attributes = new List<EntityAttribute>();
				_attributeGroups = new List<AttributeGroup>();
				if (_skeletonId == null)
				{
					var attributeSetId = _context.AttributeSets.GetDefaultAttributeSet(this.EntityType).AttributeSetId;
					if (attributeSetId != null)
					{
						_skeletonId = attributeSetId.Value;
					}
				}
			}
		}

		public async Task SaveAsync()
		{
			var attributeSet = new AttributeSet();
			attributeSet.AttributeSetName = Name;
			attributeSet.EntityTypeId = EntityType;

			if (this.Id == 0)
			{
				Debug.Assert(_skeletonId != null, nameof(_skeletonId) + " != null");
				var set = await _context.AttributeSets.Create(this.EntityType, _skeletonId.Value, attributeSet);
				this.Id = set.AttributeSetId.Value;
			}

			var currentAttributeGroups =
				_context.ProductAttributeGroups.AsQueryable().Where(group => group.AttributeSetId == Id).ToList();


			foreach (var attributeGroup in _attributeGroups)
			{
				if (!currentAttributeGroups.Select(group => group.AttributeGroupName)
					.Contains(attributeGroup.AttributeGroupName))
				{
					attributeGroup.AttributeGroupId = await _context.AttributeSets.CreateProductAttributeGroup(this.Id,
						attributeGroup.AttributeGroupName);
				}
				else
				{
					attributeGroup.AttributeGroupId = currentAttributeGroups
						.Where(group => group.AttributeGroupName == attributeGroup.AttributeGroupName)
						.Select(group => group.AttributeGroupId).SingleOrDefault();
				}
			}

			foreach (var assignment in _attributeAssignments)
			{
				var groupId = _attributeGroups.Single(group => group.AttributeGroupName == assignment.GroupName);
				await _context.AttributeSets.AssignProductAttribute(this.Id, groupId.AttributeGroupId,
					assignment.AttributeCode);
			}

			_attributeAssignments.Clear();
			await Refresh();
		}


		public void AddGroup(string groupName)
		{
			if (!_attributeGroups.Any(group => group.AttributeGroupName == groupName))
			{
				_attributeGroups.Add(new AttributeGroup {AttributeGroupName = groupName});
			}
			else
			{
				throw new InvalidOperationException("Attribute Set already contains group by this name.");
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

		public async Task Delete()
		{
			await _context.AttributeSets.Delete(Id);
		}
	}
}