using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Domain.Abstractions;
using Newtonsoft.Json;
using Serilog;

namespace Magento.RestClient.Domain.Models.EAV
{
	[DebuggerDisplay("{AttributeCode}", Name = "{DefaultFrontendLabel}")]
	public class AttributeModel : IDomainModel
	{
		private readonly IAdminContext _context;
		private AttributeFrontendInput _frontendInput;
		private bool _frontendInputChanged;
		private List<Option> _options;

		public List<string> Options {
			get => _options.Select(option => option.Label).ToList();
			set => _options = value.Select(s => new Option() { Label = s }).ToList();
		}

		public AttributeModel(IAdminContext context, string attributeCode, string label = "")
		{
			_context = context;
			this.AttributeCode = attributeCode;
			this.DefaultFrontendLabel = label;
			Refresh().GetAwaiter().GetResult();
		}

		public AttributeModel(IAdminContext context)
		{
			_context = context;
			Refresh().GetAwaiter().GetResult();
		}

		[JsonIgnore] public AttributeModelValidator Validator { get; set; } = new AttributeModelValidator();

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
			ProductAttribute existing = null;
			if (!string.IsNullOrWhiteSpace(AttributeCode))
			{
				existing = await _context.Attributes.GetByCode(this.AttributeCode).ConfigureAwait(false);
			}

			if (existing == null)
			{
				this.IsPersisted = false;
				_options = new List<Option>();
			}
			else
			{
				this.IsPersisted = true;

				this.DefaultFrontendLabel = existing.DefaultFrontendLabel;
				if (existing.FrontendInput != null)
				{
					_frontendInput = existing.FrontendInput.Value;
				}

				_options = await _context.Attributes.GetProductAttributeOptions(this.AttributeCode)
					.ConfigureAwait(false);
			}
		}


		public async Task SaveAsync()
		{
			await this.Validator.ValidateAndThrowAsync(this).ConfigureAwait(false);

			var existing = await _context.Attributes.GetByCode(this.AttributeCode).ConfigureAwait(false);


			var attribute = GetAttribute();
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


				foreach (var option in _options)
				{
					if (!existingOptions.Select(existingOption => existingOption.Label).Any(s => s.Equals(option.Label, StringComparison.InvariantCultureIgnoreCase)))
					{
						Log.Information("Creating option {AttributeCode}:{Label}", this.AttributeCode, option.Label);
						await _context.Attributes.CreateProductAttributeOption(this.AttributeCode, option)
							.ConfigureAwait(false);
					}
					else if (!_options.Select(o => o.Label).Contains(option.Label) &&
					         !string.IsNullOrEmpty(option.Value))
					{
						Log.Information("Deleting option {AttributeCode}:{Label}", this.AttributeCode, option.Label);


						await _context.Attributes.DeleteProductAttributeOption(this.AttributeCode, option.Value)
							.ConfigureAwait(false);
					}
				}
			}

			await Refresh().ConfigureAwait(false);
		}

		public Task Delete()
		{
			return _context.Attributes.DeleteProductAttribute(this.AttributeCode);
		}

		public void AddOptions(params string[] options)
		{
			foreach (var option in options)
			{
				AddOption(option);
			}
		}

		public void AddOption(string option)
		{
			if (option != null)
			{
				if (option.Trim().Equals("0"))
				{
					_options.Add(new Option { Label = "0 (Zero)" });

					//throw new Exception("Magento does not allow 0 as an attribute option value.");
				}
				else if (_options.All(o => o.Label != option))
				{
					_options.Add(new Option { Label = option });
				}
			}
		}

		public ProductAttribute GetAttribute()
		{
			return new ProductAttribute(this.AttributeCode) {
				IsRequired = this.Required,
				IsVisible = this.Visible,
				DefaultFrontendLabel = this.DefaultFrontendLabel,
				FrontendInput = this.FrontendInput,
				Options = this.Options.Select(s => new Option() { Label = s }).ToList()
			};
		}
	}
}