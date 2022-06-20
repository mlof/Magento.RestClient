using System.Threading.Tasks;
using Magento.RestClient.Modules.Quote.Abstractions;
using Magento.RestClient.Modules.Quote.Models;

namespace Magento.RestClient.Modules.Quote
{
	public static class CartRepositoryExtensions
	{
		public static Task<CartItem> AddItemToCart(this ICartRepository cartRepository, long cartId, string sku,
			long quantity)
		{
			return cartRepository.AddItemToCart(cartId, new CartItem {QuoteId = cartId, Sku = sku, Qty = quantity}
			);
		}
	}
}