using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public class ProductGalleryModel : IDomainModel
	{
		private readonly IAdminContext _context;

		public ProductGalleryModel(IAdminContext context, string sku)
		{
			this.Sku = sku;
			_context = context;
			Refresh().GetAwaiter().GetResult();
		}

		public string Sku { get; }

		public bool IsPersisted { get; }

		public async Task Refresh()
		{
			var media = await this._context.ProductMedia.GetForSku(this.Sku);
			if (media != null)
			{
				this.Items = media;
			}
			else
			{
				this.Items = new List<ProductMedia>();
			}
		}

		public List<ProductMedia> Items { get; set; }

		public async Task SaveAsync()
		{
			foreach (var item in Items)
			{
				if (item.Id == null)
				{
					await _context.ProductMedia.Create(Sku, item);
				}
			}

			await this.Refresh();
		}

		public async Task Delete()
		{
			throw new NotImplementedException();
		}

		public void AddByFilename(string fileName, FileInfo fileInfo)
		{
			
			if (!Items.Any(media => media.Label == fileName))
			{
				Items.Add(new ProductMedia() {
					Label = fileName,
					Disabled = false,
					MediaType = ProductMediaType.Image,
					Content = new ProductMediaContent(fileInfo)
				});
			}
		}
	}
}