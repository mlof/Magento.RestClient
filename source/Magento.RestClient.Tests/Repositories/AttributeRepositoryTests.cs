using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Extensions;

namespace Magento.RestClient.Tests.Repositories
{
	[TestFixture]
	public class AttributeRepositoryTests : AbstractAdminTest
	{
		private IAttributeRepository attributeRepository;
		private AttributeSet attributeSet;
		private string attributeCode = "testattribute";
		private string attributeSetName;
		private long attributeSetId => attributeSet.AttributeSetId.Value;

		[SetUp]
		async public Task SetupAttributes()
		{
			this.attributeSetName = "AttributeTestSet";
			this.attributeRepository = this.Context.Attributes;
			this.attributeSet = await this.Context.AttributeSets.Create(EntityType.CatalogProduct, 4,
				new AttributeSet() {AttributeSetName = attributeSetName});
		}



		[TearDown]
		public void TeardownAttributes()
		{
			var id = Context.AttributeSets.AsQueryable().SingleOrDefault(set =>
					set.AttributeSetName == attributeSetName && set.EntityTypeId == EntityType.CatalogProduct)?
				.AttributeSetId;
			this.Context.AttributeSets.Delete(id.Value);
			this.Context.Attributes.DeleteProductAttribute(attributeCode);
		}


		[Test]
		async public Task GetProductAttributes_NewAttributeSet_HasDefaultAttributes()
		{
			var defaultAttributeSet = Context.AttributeSets.GetDefaultAttributeSet(EntityType.CatalogProduct);
			var defaultAttributes = await attributeRepository.GetProductAttributes(
				defaultAttributeSet.AttributeSetId.Value);
			// Act
			var result = await attributeRepository.GetProductAttributes(
				attributeSetId);

			// Assert
			foreach (var attribute in defaultAttributes)
			{
				result.Should()
					.ContainSingle(entityAttribute => entityAttribute.AttributeCode == attribute.AttributeCode);
			}
		}

		[Test]
		async public Task Create_ValidAttribute()
		{
			// Arrange
			ProductAttribute attribute = new ProductAttribute(this.attributeCode);
			attribute.DefaultFrontendLabel = this.attributeCode;

			// Act
			var result = await attributeRepository.Create(
				attribute);

			// Assert

			var assert = Context.AttributeSets.Get(result.AttributeId);
		}

		[Test]
		async public Task DeleteProductAttribute()
		{
			// Arrange

			ProductAttribute deleteThis = new ProductAttribute("deletethis");
			deleteThis.DefaultFrontendLabel = "deletethis";

			var result = await attributeRepository.Create(
				deleteThis);

			// Act 
			await attributeRepository.DeleteProductAttribute(
				result.AttributeCode);


			// Assert

			var assert = Context.AttributeSets.Get(result.AttributeId);
			assert.Should().BeNull();
			Assert.Fail();
		}

		[Test]
		public void GetProductAttributeOptions_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			string attributeCode = null;

			// Act
			var result = attributeRepository.GetProductAttributeOptions(
				attributeCode);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void CreateProductAttributeOption_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			string attributeCode = null;
			Option option = null;

			// Act
			var result = attributeRepository.CreateProductAttributeOption(
				attributeCode,
				option);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void GetByCode_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			string attributeCode = null;

			// Act
			var result = attributeRepository.GetByCode(
				attributeCode);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void Update_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			string attributeCode = null;
			ProductAttribute attribute = null;

			// Act
			var result = attributeRepository.Update(
				attributeCode,
				attribute);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void DeleteProductAttributeOption_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			string attributeCode = null;
			string optionValue = null;

			// Act
			attributeRepository.DeleteProductAttributeOption(
				attributeCode,
				optionValue);

			// Assert
			Assert.Fail();
		}
	}
}