using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Extensions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class QueryableTests : AbstractAdminTest
	{

		[Test]
		public void CanGetDefaultAttributeSet()
		{
			Context.AttributeSets.GetDefaultAttributeSet(EntityType.CatalogProduct);
		}
	}
}