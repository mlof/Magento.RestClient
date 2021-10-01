using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
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
			Refresh().GetAwaiter().GetResult();
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

		public async Task Refresh()
		{
			var tree = await _context.Categories.GetCategoryTree(this.Id).ConfigureAwait(false);

			this.Id = tree.Id;
			this.ParentId = tree.ParentId;
			_children = tree.ChildrenData.ToList();
			var productsResponse = await _context.Categories.GetProducts(this.Id).ConfigureAwait(false);
			_products = productsResponse.ToList();
		}

		public async Task SaveAsync()
		{
			var model = new Category {Name = this.Name};
			// don't update the root category.
			if (this.ParentId != 0)
			{
				if (this.IsPersisted)
				{
					await _context.Categories.UpdateCategory(this.Id, model).ConfigureAwait(false);
				}
				else
				{
					var response = _context.Categories.CreateCategory(model);
					this.Id = response.Id;
				}
			}

			foreach (var child in _children)
			{
				child.ParentId = this.Id;
				if (child.Id == 0)
				{
					await _context.Categories.CreateCategory(child).ConfigureAwait(false);
				}
				else
				{
					await _context.Categories.UpdateCategory(child.Id, child).ConfigureAwait(false);
				}
			}

			foreach (var link in _products)
			{
				await _context.Categories.AddProduct(this.Id, link).ConfigureAwait(false);
			}

			await Refresh().ConfigureAwait(false);
		}

		public Task Delete()
		{
			return _context.Categories.DeleteCategoryById(this.Id);
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