using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Category;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public class CategoryModel : ICategoryModel
	{
		private readonly IAdminContext _context;
		private List<ICategoryModel> _children = new List<ICategoryModel>();
		private List<ProductLink> _products = new List<ProductLink>();


		public CategoryModel(IAdminContext context)
		{
			this._context = context;

			var root = context.Categories.GetCategoryTree().GetAwaiter().GetResult();
			this.Id = root.Id;


			Refresh().GetAwaiter().GetResult();
		}

		public CategoryModel(IAdminContext context, long id)
		{
			_context = context;
			this.Id = id;
			Refresh().GetAwaiter().GetResult();
		}

		public CategoryModel(IAdminContext context, string name)
		{
			_context = context;
			this.Name = name;
			Refresh().GetAwaiter().GetResult();
		}

		public long? ParentId { get; private set; }

		public void SetParentId(long id)
		{
			this.ParentId = id;
		}

		public long? Id {
			get;
			private set;
		}

		public string Name {
			get;
			private set;
		}

		public IReadOnlyList<ICategoryModel> Children => _children.AsReadOnly();

		public IReadOnlyList<ProductLink> Products => _products.AsReadOnly();

		public bool IsPersisted => this.Id != null && this.Id != 0;

		public async Task Refresh()
		{
			if (this.Id != null)
			{
				var tree = await _context.Categories.GetCategoryTree(this.Id).ConfigureAwait(false);

				this.Id = tree.Id;
				this.Name = tree.Name;
				this.ParentId = tree.ParentId;
				_children = tree.ChildrenData.Select(category => new CategoryModel(this._context, category.Id))
					.ToList<ICategoryModel>();
				var productsResponse = await _context.Categories.GetProducts(this.Id.Value).ConfigureAwait(false);
				_products = productsResponse.ToList();
			}
		}

		public async Task SaveAsync()
		{
			var category = new Category { Name = this.Name, ParentId = this.ParentId, IsActive = this.IsActive };
			// don't update the root category.
			if (this.ParentId != 0)
			{
				if (this.IsPersisted)
				{
					var id = this.Id;
					if (id != null)
					{
						await _context.Categories.UpdateCategory(id.Value, category).ConfigureAwait(false);
					}
				}
				else
				{
					var response = await _context.Categories.CreateCategory(category);
					this.Id = response.Id;
				}
			}

			foreach (var child in _children)
			{
				if (this.Id != null)
				{
					child.SetParentId(this.Id.Value);
				}

				await child.SaveAsync();
			}


			await Refresh().ConfigureAwait(false);
		}

		public Task Delete()
		{
			var id = this.Id;
			if (id != null)
			{
				return _context.Categories.DeleteCategoryById(id.Value);
			}

			return Task.CompletedTask;
		}

		public ICategoryModel GetOrCreateChild(string name)
		{
			if (_children.Any(c => c.Name == name))
			{
				return _children.SingleOrDefault(model => model.Name == name);
			}
			else
			{
				var child = new CategoryModel(this._context, name);
				child.ParentId = this.Id;
				child.IsActive = true;
				_children.Add(child);

				return child;
			}
		}

		public bool IsActive { get; set; }

		public void AddProduct(string productSku)
		{
			_products.Add(new ProductLink { Sku = productSku, CategoryId = this.Id.Value });
		}

		public ICategoryModel this[string name] {
			get => GetOrCreateChild(name);
			set {
				if (_children.Any(model => model.Name == value.Name))
				{
					_children.Remove(_children.Single(model => model.Name == value.Name));
				}

				_children.Add(value);
			}
		}
	}
}