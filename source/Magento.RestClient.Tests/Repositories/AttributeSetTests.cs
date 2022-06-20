using System.Linq;
using FluentAssertions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Exceptions.Generic;
using Magento.RestClient.Modules.EAV.Model;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class AttributeSetTests : AbstractAdminTest
	{
		[SetUp]
		public void AttributeSetSetup()
		{
			var defaultProductAttributeSet = 4;

			var laptopAttributeSet = new AttributeSet() {
				EntityTypeId = EntityType.CatalogProduct, AttributeSetName = "Laptops"
			};

			MagentoContext.AttributeSets.Create(EntityType.CatalogProduct, defaultProductAttributeSet, laptopAttributeSet);
		}


		[TearDown]
		public void AttributeSetTearDown()
		{
			var attributeSet = MagentoContext.AttributeSets.AsQueryable().SingleOrDefault(attributeSet =>
				attributeSet.AttributeSetName == "Laptops" &&
				attributeSet.EntityTypeId == EntityType.CatalogProduct);
			MagentoContext.AttributeSets.Delete(attributeSet.AttributeSetId.Value);
		}

		[Test]
		public void Search_Existing()
		{
			var items = MagentoContext.AttributeSets.AsQueryable().Where(attributeSet =>
				attributeSet.AttributeSetName == "Laptops" &&
				attributeSet.EntityTypeId == EntityType.CatalogProduct).ToList();

			items.Should().HaveCount(1);
		}

		[Test]
		public void Search_NonExistent()
		{
			var response = MagentoContext.AttributeSets.AsQueryable().Where(set => set.AttributeSetName == "Hamsters").ToList();
			response.Should().HaveCount(0);
		}

		
		[Test]
		public void GetProductAttributes_NonExistent()
		{
			Assert.Throws<EntityNotFoundException>(() => {
				var attributes = MagentoContext.Attributes.GetProductAttributes(-1);
			});
		}
	}
}