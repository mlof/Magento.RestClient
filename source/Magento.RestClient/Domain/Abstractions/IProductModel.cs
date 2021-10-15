using System.Collections.Generic;
using Magento.RestClient.Data.Models.Catalog.Products;

namespace Magento.RestClient.Domain.Abstractions
{
	public interface IProductModel
	{
		public List<MediaEntry> MediaEntries { get; set; } 
		string Sku { get; }

		Product GetProduct();
	}
}