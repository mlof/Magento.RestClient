using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Extensions;

namespace Magento.RestClient.Domain.Models.EAV
{
	public class AttributeSetModel : IDomainModel
	{
		private readonly IAdminContext _context;
		private List<AttributeGroupModel> _attributeGroups;
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

		public AttributeSetModel(IAdminContext adminContext, long id)
		{
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
				Debug.Assert(r.AttributeSetId != null, "r.AttributeSetId != null");
				this.Id = r.AttributeSetId.Value;

				var model = await _context.AttributeSets.Get(this.Id).ConfigureAwait(false);

				this.Name = model.AttributeSetName;

				var attributesResponse = await _context.Attributes.GetProductAttributes(this.Id).ConfigureAwait(false);

				this.Attributes =
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

				this.Attributes = new List<AttributeModel>().AsReadOnly();
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
			var attributeSet = new AttributeSet { AttributeSetName = this.Name, EntityTypeId = this.EntityType };

			if (this.Id == 0)
			{
				Debug.Assert(_skeletonId != null, nameof(_skeletonId) + " != null");
				var set = await _context.AttributeSets.Create(this.EntityType, _skeletonId.Value, attributeSet)
					.ConfigureAwait(false);
				Debug.Assert(set.AttributeSetId != null, "set.AttributeSetId != null");
				this.Id = set.AttributeSetId.Value;
			}


			foreach (var attributeGroup in _attributeGroups)
			{
				if (attributeGroup.AttributeSetId == 0)
				{
					attributeGroup.AttributeSetId = this.Id;
				}

				await attributeGroup.SaveAsync().ConfigureAwait(false);
			}


			await Refresh().ConfigureAwait(false);
		}


		public Task Delete()
		{
			return _context.AttributeSets.Delete(this.Id);
		}

		public AttributeGroupModel this[string groupName] {
			get {
				if (this.AttributeGroups.All(group => @group.Name != groupName))
				{
					_attributeGroups.Add(new AttributeGroupModel(this._context, this.Id, groupName));
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