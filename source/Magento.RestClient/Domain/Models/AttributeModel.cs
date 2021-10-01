using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;
using Serilog;

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
			this.Validator = new AttributeModelValidator();
			_context = context;
			this.AttributeCode = attributeCode;
			Refresh().GetAwaiter().GetResult();
		}

		public AttributeModelValidator Validator { get; set; }

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
			var existing = await _context.Attributes.GetByCode(this.AttributeCode).ConfigureAwait(false);

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

				_options = await _context.Attributes.GetProductAttributeOptions(this.AttributeCode)
					.ConfigureAwait(false);
			}
		}

	

		public async Task SaveAsync()
		{
			await Validator.ValidateAndThrowAsync(this).ConfigureAwait(false);

			var existing = await _context.Attributes.GetByCode(this.AttributeCode).ConfigureAwait(false);
			var attribute =
				new ProductAttribute(this.AttributeCode) {
					IsRequired = this.Required,
					IsVisible = this.Visible,
					DefaultFrontendLabel = this.DefaultFrontendLabel,
					FrontendInput = this.FrontendInput
				};
			if (existing != null && _frontendInputChanged)
			{
				await _context.Attributes.DeleteProductAttribute(this.AttributeCode).ConfigureAwait(false);
			}

			attribute = existing != null
				? await _context.Attributes.Update(this.AttributeCode, attribute).ConfigureAwait(false)
				: await _context.Attributes.Create(attribute).ConfigureAwait(false);

			if (_options.Any())
			{
				var existingOptions = await _context.Attributes.GetProductAttributeOptions(this.AttributeCode)
					.ConfigureAwait(false);

				foreach (var option in _options.Where(option =>
					!existingOptions.Select(option1 => option1.Label).Contains(option.Label)))
				{
					await _context.Attributes.CreateProductAttributeOption(this.AttributeCode, option)
						.ConfigureAwait(false);
				}

				foreach (var option in existingOptions.Where(option =>
					!_options.Select(o => o.Label).Contains(option.Label) && !string.IsNullOrEmpty(option.Value)))
				{
					await _context.Attributes.DeleteProductAttributeOption(this.AttributeCode, option.Value)
						.ConfigureAwait(false);
				}
			}

			await Refresh().ConfigureAwait(false);
		}

		public Task Delete()
		{
			return _context.Attributes.DeleteProductAttribute(this.AttributeCode);
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