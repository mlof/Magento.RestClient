using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class AttributeModel : IDomainModel
	{
		public string AttributeCode => _model.AttributeCode;
		private readonly IAdminClient _client;
		private ProductAttribute _model;
		private List<Option> _options;
		private bool _frontendInputChanged;

		public AttributeModel(IAdminClient client, string attributeCode)
		{
			this._client = client;
			var existing = _client.Attributes.GetByCode(attributeCode);

			if (existing == null)
			{
				this._model = new ProductAttribute() {AttributeCode = attributeCode};
				this._options = new List<Option>();
				
			}
			else
			{
				this._model = existing;
				Refresh();

			}
		}


		public void AddOption(string option)
		{
			if (option != null)
			{


				if (_options.All(o => o.Label != option))
				{
					_options.Add(new Option() {Label = option});
				}
			}
		}

		public void SetFrontendLabel(string frontendLabel)
		{
			_model.DefaultFrontendLabel = frontendLabel;
		}

		public void SetFrontendInput(string frontendInput)
		{
			if (_model.FrontendInput != frontendInput)
			{
				_model.FrontendInput = frontendInput;
				this._frontendInputChanged = true;
			}
		}

		public void Refresh()
		{
			this._options = _client.Attributes.GetProductAttributeOptions(AttributeCode);
		}

		public void Save()
		{
			var existing = _client.Attributes.GetByCode(AttributeCode);

			if (existing!= null && _frontendInputChanged)
			{
				_client.Attributes.DeleteProductAttribute(AttributeCode);
			}

			if (existing != null)
			{
				_model = _client.Attributes.Update(AttributeCode, _model);
			}
			else
			{
				_model = _client.Attributes.Create(_model);
			}

			if (_options.Any())
			{
				var existingOptions = _client.Attributes.GetProductAttributeOptions(AttributeCode);

				foreach (var option in _options.Where(option =>
					!existingOptions.Select(option1 => option1.Label).Contains(option.Label)))
				{
					_client.Attributes.CreateProductAttributeOption(AttributeCode, option);
				}

				foreach (var option in existingOptions.Where(option =>
					!_options.Select(o => o.Label).Contains(option.Label) && !string.IsNullOrEmpty(option.Value)))
				{
					_client.Attributes.DeleteProductAttributeOption(AttributeCode, option.Value);
				}
			}
			Refresh();
		}
	}
}