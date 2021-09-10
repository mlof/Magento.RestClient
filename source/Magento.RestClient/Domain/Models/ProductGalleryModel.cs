﻿using System;
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

		public void Refresh()
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}
	}
}