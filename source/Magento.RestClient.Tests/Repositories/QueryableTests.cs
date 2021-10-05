using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Extensions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class QueryableTests : AbstractIntegrationTest
	{

		[Test]
		public void CanGetDefaultAttributeSet()
		{
			Context.AttributeSets.GetDefaultAttributeSet(EntityType.CatalogProduct);
		}
	}
}