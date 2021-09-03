using System.Linq;
using FluentAssertions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
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
			var response = Context.Search.AttributeSets(builder => builder
				.WithPageSize(100)
				.WithPage(1)
				.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct)
				.WhereEquals(set => set.AttributeSetName, "Laptops"));

			var attributeSet = response.Items.First();
			Context.AttributeSets.Delete(attributeSet.AttributeSetId.Value);
		}

		[Test]
		public void Search_Existing()
		{
			var response = Context.Search.AttributeSets(builder => builder
				.WithPageSize(100)
				.WithPage(1)
				.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct)
				.WhereEquals(set => set.AttributeSetName, "Laptops"));

			response.Items.Should().HaveCount(1);
		}

		[Test]
		public void Search_NonExistent()
		{
			var response = Context.Search.AttributeSets(builder => builder
				.WithPageSize(100)
				.WithPage(1)
				.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct)
				.WhereEquals(set => set.AttributeSetName, "Hamsters"));

			response.Items.Should().HaveCount(0);
		}

		[Test]
		public void GetProductAttributes_Existent()
		{
			var response = Context.Search.AttributeSets(builder => builder
				.WithPageSize(100)
				.WithPage(1)
				.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct));

			var attributeSet = response.Items.First();


			var attributes = Context.Attributes.GetProductAttributes(attributeSet.AttributeSetId.Value);

			attributes.Should().NotBeEmpty();
		}

		[Test]
		public void GetProductAttributeGroups_Existent()
		{
			var response = Context.Search.AttributeSets(builder => builder
				.WithPageSize(100)
				.WithPage(1)
				.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct));

			var attributeSet = response.Items.First();



			//attributes.Should().NotBeEmpty();
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