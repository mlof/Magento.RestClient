using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Data.Models.Category;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class CategoryModel : IDomainModel
	{
		private readonly IAdminContext _context;
		private List<Category> _children;
		private List<ProductLink> _products;

		public CategoryModel(IAdminContext context)
		{
			_context = context;
		}

		public CategoryModel(IAdminContext context, long id)
		{
			_context = context;
			this.Id = id;
			Refresh();
		}

		public long ParentId { get; private set; }

		public long Id {
			get;
			private set;
		}

		public string Name {
			get;
			private set;
		}

		public IReadOnlyList<Category> Children => _children.AsReadOnly();

		public IReadOnlyList<ProductLink> Products => _products.AsReadOnly();

		public bool IsPersisted => this.Id != 0;

		public void Refresh()
		{
			var tree = _context.Categories.GetCategoryTree(this.Id);

			this.Id = tree.Id;
			this.ParentId = tree.ParentId;
			_children = tree.ChildrenData.ToList();
			_products = _context.Categories.GetProducts(this.Id).ToList();
		}

		public void Save()
		{
			var _model = new Category {Name = this.Name};
			// don't update the root category.
			if (this.ParentId != 0)
			{
				if (this.IsPersisted)
				{
					_context.Categories.UpdateCategory(this.Id, _model);
				}
				else
				{
					var response = _context.Categories.CreateCategory(_model);
					this.Id = response.Id;
				}
			}

			foreach (var child in _children)
			{
				child.ParentId = this.Id;
				if (child.Id == 0)
				{
					_context.Categories.CreateCategory(child);
				}
				else
				{
					_context.Categories.UpdateCategory(child.Id, child);
				}
			}

			foreach (var link in _products)
			{
				_context.Categories.AddProduct(this.Id, link);
			}


			Refresh();
		}

		public void Delete()
		{
			_context.Categories.DeleteCategoryById(this.Id);
		}

		public void AddChild(string name, bool isActive = true)
		{
			if (!_children.Any(c => c.Name == name))
			{
				_children.Add(new Category {Name = name, IsActive = isActive});
			}
		}

		public void AddProduct(string productSku)
		{
			_products.Add(new ProductLink {Sku = productSku, CategoryId = this.Id});
		}
	}
}