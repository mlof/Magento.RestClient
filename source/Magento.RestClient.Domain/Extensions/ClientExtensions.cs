using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Models.Cart;

namespace Magento.RestClient.Domain.Extensions
{
	public static class ClientExtensions
	{
	

		public static CartModel CreateNewCartModel(this IAdminContext context)
		{
			return new(context);
		}

		public static CartModel GetExistingCartModel(this IAdminContext context, long id)
		{
			return new(context, id);
		}
	}
}