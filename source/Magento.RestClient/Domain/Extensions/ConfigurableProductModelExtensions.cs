using Magento.RestClient.Domain.Models;

namespace Magento.RestClient.Domain.Extensions
{
	public static class ConfigurableProductModelExtensions
	{
		public static void AddChild(this ConfigurableProductModel configurableProduct, ProductModel model)
		{
			configurableProduct.AddChild(model.Sku);
		}
	}
}