using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class AttributeModel : IDomainModel
	{
		private readonly IAdminContext _context;
		private AttributeFrontendInput _frontendInput;
		private bool _frontendInputChanged;
		private List<Option> _options;

		public AttributeModel(IAdminContext context, string attributeCode)
		{
			_context = context;
			this.AttributeCode = attributeCode;
			Refresh().GetAwaiter().GetResult();
		}

		public string AttributeCode { get; }
		public bool Required { get; set; }
		public bool Visible { get; set; }
		public bool Searchable { get; set; }
		public bool UseInLayeredNavigation { get; set; }
		public bool Comparable { get; set; }

		public AttributeFrontendInput FrontendInput {
			get => _frontendInput;
			set {
				if (_frontendInput != value)
				{
					_frontendInputChanged = true;
				}

				_frontendInput = value;
			}
		}

		public string DefaultFrontendLabel { get; set; }


		public bool IsPersisted { get; private set; }

		public async Task Refresh()
		{
			var existing = await _context.Attributes.GetByCode(this.AttributeCode);

			if (existing == null)
			{
				this.IsPersisted = false;
				_options = new List<Option>();
			}
			else
			{
				this.IsPersisted = true;

				this.DefaultFrontendLabel = existing.DefaultFrontendLabel;
				_frontendInput = existing.FrontendInput.Value;

				_options = await _context.Attributes.GetProductAttributeOptions(this.AttributeCode);
			}
		}

		public async Task SaveAsync()
		{
			var existing = await _context.Attributes.GetByCode(this.AttributeCode);
			var attribute = new ProductAttribute(this.AttributeCode);
			attribute.IsRequired = Required;
			attribute.IsVisible = Visible;
			attribute.DefaultFrontendLabel = DefaultFrontendLabel;
			attribute.FrontendInput = this.FrontendInput;
			if (existing != null && _frontendInputChanged)
			{
				await _context.Attributes.DeleteProductAttribute(this.AttributeCode);
			}

			attribute = existing != null
				? await _context.Attributes.Update(this.AttributeCode, attribute)
				: await _context.Attributes.Create(attribute);

			if (_options.Any())
			{
				var existingOptions = await _context.Attributes.GetProductAttributeOptions(this.AttributeCode);

				foreach (var option in _options.Where(option =>
					!existingOptions.Select(option1 => option1.Label).Contains(option.Label)))
				{
					await _context.Attributes.CreateProductAttributeOption(this.AttributeCode, option);
				}

				foreach (var option in existingOptions.Where(option =>
					!_options.Select(o => o.Label).Contains(option.Label) && !string.IsNullOrEmpty(option.Value)))
				{
					await _context.Attributes.DeleteProductAttributeOption(this.AttributeCode, option.Value);
				}
			}

			await Refresh();
		}

		public async Task Delete()
		{
			await _context.Attributes.DeleteProductAttribute(AttributeCode);
		}


		public void AddOption(string option)
		{
			if (option != null)
			{
				if (option.Trim().Equals("0"))
				{
					_options.Add(new Option {Label = "0 (Zero)"});

					//throw new Exception("Magento does not allow 0 as an attribute option value.");
				}
				else if (_options.All(o => o.Label != option))
				{
					_options.Add(new Option {Label = option});
				}
			}
		}
	}
}