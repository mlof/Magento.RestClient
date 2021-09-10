using System.Linq;
using FluentAssertions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Exceptions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class AttributeSetTests : AbstractIntegrationTest
	{
		[SetUp]
		public void AttributeSetSetup()
		{
			var defaultProductAttributeSet = 4;

			var laptopAttributeSet = new AttributeSet() {
				EntityTypeId = EntityType.CatalogProduct, AttributeSetName = "Laptops"
			};

			Context.AttributeSets.Create(EntityType.CatalogProduct, defaultProductAttributeSet, laptopAttributeSet);
		}


		[TearDown]
		public void AttributeSetTearDown()
		{
			var attributeSet = Context.AttributeSets.SingleOrDefault(attributeSet =>
				attributeSet.AttributeSetName == "Laptops" &&
				attributeSet.EntityTypeId == EntityType.CatalogProduct);
			Context.AttributeSets.Delete(attributeSet.AttributeSetId.Value);
		}

		[Test]
		public void Search_Existing()
		{
			var items = Context.AttributeSets.Where(attributeSet =>
				attributeSet.AttributeSetName == "Laptops" &&
				attributeSet.EntityTypeId == EntityType.CatalogProduct).ToList();

			items.Should().HaveCount(1);
		}

		[Test]
		public void Search_NonExistent()
		{
			var response = Context.AttributeSets.Where(set => set.AttributeSetName == "Hamsters").ToList();
			response.Should().HaveCount(0);
		}

		
		[Test]
		public void GetProductAttributes_NonExistent()
		{
			Assert.Throws<EntityNotFoundException>(() => {
				var attributes = Context.Attributes.GetProductAttributes(-1);
			});
		}
	}
}