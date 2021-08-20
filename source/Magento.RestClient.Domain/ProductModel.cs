using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class ProductModel : IDomainModel
	{
		private readonly IProductRepository _productRepository;
		private Product _model;
		private readonly ProductValidator _productValidator;

		public ProductModel(IProductRepository productRepository, string sku)
		{
			this._productRepository = productRepository;
			this._productValidator = new ProductValidator();
			this.Scope = "all";


			var existingProduct = _productRepository.GetProductBySku(sku);
			if (existingProduct == null)
			{
				this._model = new Product(sku);
			}
			else
			{
				this._model = existingProduct;
			}
		}

		public string Sku => _model.Sku;

		public string Scope { get; private set; }
		public string Name => _model.Name;


		public ProductModel SetName(string name)
		{
			_model.Name = name;
			return this;
		}

		public ProductModel SetPrice(decimal price)
		{
			_model.Price = price;
			return this;
		}


		public ProductModel SetVisibility(ProductVisibility visibility)
		{
			_model.Visibility = (int) visibility;

			return this;
		}

		public void Delete()
		{
			_productRepository.DeleteProduct(Sku);
		}

		public ProductModel SetAttributeSet(long attributeSetId)
		{
			_model.AttributeSetId = attributeSetId;


			return this;
		}

		public ProductModel SetAttribute(string attributeCode, string value)
		{
			if (_model.CustomAttributes.Any(attribute => attribute.AttributeCode == attributeCode))
			{
				_model.CustomAttributes.Single(attribute => attribute.AttributeCode == attributeCode).Value =
					value;
			}
			else
			{
				_model.CustomAttributes.Add(new CustomAttribute(attributeCode, value));
			}

			return this;
		}

		public void Refresh()
		{
			this._model = _productRepository.GetProductBySku(Sku, this.Scope);

		}

		public void Save()
		{
			var existingProduct = _productRepository.GetProductBySku(Sku);
			if (existingProduct == null)
			{
				_productRepository.CreateProduct(_model, true);
			}
			else
			{
				_productRepository.UpdateProduct(Sku, _model, scope: Scope);
			}

			Refresh();
		}
	}

	public interface IDomainModel
	{
		public void Refresh();
		public void Save();
	}
}