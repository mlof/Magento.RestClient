using System;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class ProductGalleryModel : IDomainModel
	{
		private readonly IAdminContext _context;

		public ProductGalleryModel(IAdminContext context, string sku)
		{
			this.Sku = sku;
			_context = context;
		}

		public string Sku { get; }

		public bool IsPersisted { get; }

		public async Task Refresh()
		{
			throw new NotImplementedException();
		}

		public async Task SaveAsync()
		{
			throw new NotImplementedException();
		}

		public async Task Delete()
		{
			throw new NotImplementedException();
		}
	}
}