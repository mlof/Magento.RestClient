using Magento.RestClient.Repositories;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using FluentAssertions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Products;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Tests.Repositories
{
	[TestFixture]
	public class AttributeRepositoryTests : AbstractIntegrationTest
	{
		private IAttributeRepository attributeRepository;
		private AttributeSet attributeSet;
		private string attributeCode = "testattribute";
		private string attributeSetName;
		private long attributeSetId => attributeSet.AttributeSetId.Value;

		[SetUp]
		public void SetupAttributes()
		{
			this.attributeSetName = "AttributeTestSet";
			this.attributeRepository = this.Context.Attributes;
			this.attributeSet = this.Context.AttributeSets.Create(EntityType.CatalogProduct, 4,
				new AttributeSet() {AttributeSetName = attributeSetName});
		}

		[TearDown]
		public void TeardownAttributes()
		{
			var id = this.Context.Search.AttributeSets(builder =>
					builder.WhereEquals(set => set.AttributeSetName, attributeSetName)
						.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct))
				.Items.SingleOrDefault()
				.AttributeSetId;
			this.Context.AttributeSets.Delete(id.Value);
			this.Context.Attributes.DeleteProductAttribute(attributeCode);
		}

		[Test]
		public void GetProductAttributes_NewAttributeSet_HasDefaultAttributes()
		{
			var defaultAttributeSet = Context.Search.GetDefaultAttributeSet(EntityType.CatalogProduct);
			var defaultAttributes = attributeRepository.GetProductAttributes(
					defaultAttributeSet.AttributeSetId.Value)
				.OrderBy(attribute => attribute.AttributeCode);
			// Act
			var result = attributeRepository.GetProductAttributes(
					attributeSetId)
				.OrderBy(attribute => attribute.AttributeCode);

			// Assert
			foreach (var attribute in defaultAttributes)
			{
				result.Should()
					.ContainSingle(entityAttribute => entityAttribute.AttributeCode == attribute.AttributeCode);
			}
		}

		[Test]
		public void Create_ValidAttribute()
		{
			// Arrange
			ProductAttribute attribute = new ProductAttribute(this.attributeCode);
			attribute.DefaultFrontendLabel = this.attributeCode;

			// Act
			var result = attributeRepository.Create(
				attribute);

			// Assert

			var assert = Context.AttributeSets.Get(result.AttributeId);
		}

		[Test]
		public void DeleteProductAttribute()
		{

			// Arrange

			ProductAttribute deleteThis = new ProductAttribute("deletethis");
			deleteThis.DefaultFrontendLabel = "deletethis";

			var result = attributeRepository.Create(
				deleteThis);

			// Act 
			attributeRepository.DeleteProductAttribute(
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