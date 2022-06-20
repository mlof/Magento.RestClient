using Magento.RestClient.Extensions;
using Magento.RestClient.Modules.EAV;
using Magento.RestClient.Modules.EAV.Model;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class QueryableTests : AbstractAdminTest
	{

		[Test]
		public void CanGetDefaultAttributeSet()
		{
			MagentoContext.AttributeSets.GetDefaultAttributeSet(EntityType.CatalogProduct);
		}
	}
}