using System.Collections.Generic;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Stock;

namespace Magento.RestClient.Domain.Abstractions
{
	public interface IProductModel : IDomainModel
	{
		dynamic this[string attributeCode] { get; set; }

		public IReadOnlyList<MediaEntry> MediaEntries { get; }
		string Sku { get; }
		string Scope { get; }
		string Name { get; set; }
		ProductVisibility Visibility { get; set; }
		List<CustomAttribute> CustomAttributes { get; set; }
		decimal? Price { get; set; }
		StockItem StockItem { get; set; }
		ProductType Type { get; set; }
		string UrlKey { get; set; }
		string Description { get; set; }
		string ShortDescription { get; set; }
		IReadOnlyCollection<SpecialPrice> SpecialPrices { get; }

		void AddMediaEntry(MediaEntry entry);
		Product GetProduct();
	}
}