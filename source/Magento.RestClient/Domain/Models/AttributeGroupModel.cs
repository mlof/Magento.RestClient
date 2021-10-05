using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	[DebuggerDisplay("{Name}")]
	public class AttributeGroupModel : IDomainModel
	{
		public long AttributeSetId { get; set; }
		public long Id { get; set; }
		public string Name { get; }
		private readonly IAdminContext _context;

		public AttributeGroupModel(IAdminContext context, long attributeSetId, long id,
			string name)
		{
			_context = context;

			this.AttributeSetId = attributeSetId;
			this.Id = id;
			this.Name = name;
		}

		private readonly List<AttributeModel> _attributes = new List<AttributeModel>();

		public IReadOnlyList<AttributeModel> Attributes => _attributes.AsReadOnly();

		public AttributeGroupModel(IAdminContext context, long attributeSetId,
			string name)
		{
			_context = context;

			this.AttributeSetId = attributeSetId;
			this.Name = name;
		}

		public bool IsPersisted { get; }

		public Task Refresh()
		{
			throw new NotImplementedException();
		}

		async public Task SaveAsync()
		{
			if (this.Id == 0)
			{
				this.Id = await _context.AttributeSets.CreateProductAttributeGroup(this.AttributeSetId,
					this.Name).ConfigureAwait(false);
			}

			foreach (var attribute in this.Attributes)
			{
				await attribute.SaveAsync().ConfigureAwait(false);
				await _context.AttributeSets.AssignProductAttribute(this.AttributeSetId, this.Id,
					attribute.AttributeCode).ConfigureAwait(false);
			}
		}

		public Task Delete()
		{
			throw new NotImplementedException();
		}

		public void AssignAttributes(params string[] attributeCodes)
		{
			foreach (var attributeCode in attributeCodes)
			{
				var attribute = new AttributeModel(_context, attributeCode);
				AssignAttributes(attribute);
			}
		}

		public void AssignAttributes(params AttributeModel[] attributes)
		{
			foreach (var attribute in attributes)
			{
				_attributes.Add(attribute);
			}
		}
	}
}