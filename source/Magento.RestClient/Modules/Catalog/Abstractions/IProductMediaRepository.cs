﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Modules.Catalog.Models.Products;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IProductMediaRepository
	{
		Task<MediaEntry> Create(string sku, MediaEntry entry);
		Task<List<MediaEntry>> GetForSku(string sku);
		Task<bool> Delete(string sku, long entryId);
		Task<MediaEntry> Get(string sku, int entryId);
		Task<MediaEntry> Update(string sku, long entryId, MediaEntry entry);
	}
}