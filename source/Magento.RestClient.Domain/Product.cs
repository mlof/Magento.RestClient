using FluentValidation;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class ProductFactory
	{
		private readonly IIntegrationClient client;

		public ProductFactory(IIntegrationClient client)
		{
			this.client = client;
		}


		public Product GetExisting(string sku)
		{
			return new Product(client.Products, sku);
		}

		public Product CreateNew(string sku)
		{
			var p = new Models.Products.Product {
				Sku = sku,
				Price = 10,
				AttributeSetId = 4,
				Name = sku,
				Status = 0
			};
			client.Products.CreateProduct(p);

			return GetExisting(sku);
		}

		public static ProductFactory CreateInstance(IIntegrationClient client)
		{
			return new ProductFactory(client);
		}
	}

	public class Product
	{
		private readonly IProductRepository _productRepository;
		private string _sku;
		private Models.Products.Product _model;
		private readonly ProductValidator _productValidator;

		public Product(IProductRepository productRepository, string sku)
		{
			this._productRepository = productRepository;
			this._productValidator = new ProductValidator();

			this.Sku = sku;
		}


		public string Sku {
			get { return _sku; }
			private init {
				_sku = value;
				if (!string.IsNullOrWhiteSpace(_sku))
				{
					UpdateMagentoValues();
				}
			}
		}

		private Product UpdateMagentoValues()
		{
			this._model = _productRepository.GetProductBySku(Sku);

			return this;
		}


		public Product SetName(string name)
		{
			var p = GetMinimalProduct();
			p.Name = name;
			_productRepository.UpdateProduct(Sku, p);
			return UpdateMagentoValues();
		}

		public Product SetPrice(decimal price)
		{
			var p = GetMinimalProduct();
			p.Price = price;
			_productRepository.UpdateProduct(Sku, p);
			return UpdateMagentoValues();
		}

		private Models.Products.Product GetMinimalProduct()
		{
			return new Models.Products.Product() {
				Sku = _model.Sku, AttributeSetId = _model.AttributeSetId, Price = _model.Price, Name = _model.Name
			};
		}

		public Product SetVisibility(ProductVisibility visibility)
		{
			var p = GetMinimalProduct();
			p.Visibility = (int) visibility;
			_productRepository.UpdateProduct(Sku, p);


			return UpdateMagentoValues();
		}
	}

	public enum ProductVisibility
	{
		NotVisibleIndividually = 1,
		Catalog = 2,
		Search = 3,
		Both = 4
	}

	internal class ProductValidator : AbstractValidator<Models.Products.Product>
	{
		public ProductValidator()
		{
			RuleFor(product => product.Sku).NotEmpty();
			RuleFor(product => product.Price).NotEmpty();
			RuleFor(product => product.AttributeSetId).NotEmpty();
			RuleFor(product => product.Price).NotNull();
			RuleFor(product => product.Name).NotEmpty();
		}
	}
}