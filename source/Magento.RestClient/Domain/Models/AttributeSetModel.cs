using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private List<AttributeGroupModel> _attributeGroups;
		private long? _skeletonId;
		private readonly AttributeSetModelValidator _validator;

		public AttributeSetModel(IAdminContext adminContext, string name, EntityType entityType,
			long? skeletonId = null)
		{
			this._validator = new AttributeSetModelValidator();
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

		public AttributeSetModel(IAdminContext adminContext, long id)
		{
			this._validator = new AttributeSetModelValidator();
			_context = adminContext;


			this.Id = id;


			Refresh().GetAwaiter().GetResult();
		}

		private EntityType EntityType { get; }

		public IReadOnlyList<AttributeModel> Attributes {
			get;
			private set;
		}

		public IReadOnlyList<AttributeGroupModel> AttributeGroups => _attributeGroups.AsReadOnly();

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

				var model = await _context.AttributeSets.Get(this.Id).ConfigureAwait(false);

				this.Name = model.AttributeSetName;

				var attributesResponse = await _context.Attributes.GetProductAttributes(this.Id).ConfigureAwait(false);

				Attributes =
					attributesResponse.Select(attribute => new AttributeModel(_context, attribute.AttributeCode))
						.ToList().AsReadOnly();
				var attributeGroups =
					_context.ProductAttributeGroups.AsQueryable().Where(group => group.AttributeSetId == this.Id)
						.ToList();
				_attributeGroups = attributeGroups.Select(group => new AttributeGroupModel(this._context,
					group.AttributeSetId, group.AttributeGroupId, group.AttributeGroupName)).ToList();
			}
			else
			{
				this.IsPersisted = false;

				Attributes = new List<AttributeModel>().AsReadOnly();
				_attributeGroups = new List<AttributeGroupModel>();
				if (_skeletonId == null)
				{
					var attributeSetId = _context.AttributeSets.GetDefaultAttributeSet(this.EntityType).AttributeSetId;
					if (attributeSetId != null)
					{
						_skeletonId = attributeSetId.Value;
					}

					var attributeGroups =
						_context.ProductAttributeGroups.AsQueryable()
							.Where(group => group.AttributeSetId == attributeSetId).ToList();
					_attributeGroups = attributeGroups.Select(group => new AttributeGroupModel(this._context,
						group.AttributeSetId, group.AttributeGroupId, group.AttributeGroupName)).ToList();
				}
			}
		}

		public async Task SaveAsync()
		{
			var attributeSet = new AttributeSet();
			attributeSet.AttributeSetName = this.Name;
			attributeSet.EntityTypeId = this.EntityType;

			if (this.Id == 0)
			{
				Debug.Assert(_skeletonId != null, nameof(_skeletonId) + " != null");
				var set = await _context.AttributeSets.Create(this.EntityType, _skeletonId.Value, attributeSet)
					.ConfigureAwait(false);
				this.Id = set.AttributeSetId.Value;
			}


			foreach (var attributeGroup in _attributeGroups)
			{

				if (attributeGroup.AttributeSetId == 0)
				{
					attributeGroup.AttributeSetId = Id;
				}

				await attributeGroup.SaveAsync();
			}


			_attributeAssignments.Clear();
			await Refresh().ConfigureAwait(false);
		}


		public async Task Delete()
		{
			await _context.AttributeSets.Delete(this.Id).ConfigureAwait(false);
		}

		public AttributeGroupModel this[string groupName] {
			get {
				if (this.AttributeGroups.All(group => @group.Name != groupName))
				{
					_attributeGroups.Add(new AttributeGroupModel(this._context, Id, groupName));
				}


				return _attributeGroups.SingleOrDefault(model => model.Name == groupName);
			}
			set {
				var index = this._attributeGroups.FindIndex(model => model.Name == groupName);
				_attributeGroups[index] = value;
			}
		}
	}
}