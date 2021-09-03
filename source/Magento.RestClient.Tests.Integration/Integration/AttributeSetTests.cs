using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Magento.RestClient.Domain;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public class AttributeSetTests : AbstractIntegrationTest
	{
		private List<AttributeFixture>? attributeFixtures;
		private List<AttributeSetFixture>? attributeSetFixtures;

		[SetUp]
		public void AttributeSetSetup()
		{
			var attributes = File.ReadAllText(Path.Join("Fixtures", "luma", "attributes.json"));
			var attributeSets = File.ReadAllText(Path.Join("Fixtures","luma", "attribute-sets.json"));
			this.attributeSetFixtures = JsonConvert.DeserializeObject<List<AttributeSetFixture>>(attributeSets);
			this.attributeFixtures = JsonConvert.DeserializeObject<List<AttributeFixture>>(attributes);
		}


		[Test]
		public void CreateFixtures()
		{

			
			/*
			foreach (var fixture in attributeFixtures)
			{
				var attribute = Client.GetAttributeModel(fixture.AttributeCode);
				attribute.SetFrontendLabel(fixture.FrontendLabel);
				attribute.SetFrontendInput(fixture.FrontendInput);

				if (fixture.Option != null)
				{
					foreach (var option in fixture.Option)
					{
						attribute.AddOption(option);
					}
				}

				try
				{
					attribute.Save();

				}
				catch
				{

				}
			}*/


			/*
			foreach (var fixture in attributeSetFixtures)
			{
				var attributeSet = Client.GetAttributeSetModel(fixture.AttributeSetName);
				
				foreach (var attributeGroup in fixture.AttributeGroups)
				{
					attributeSet.AddGroup(attributeGroup.AttributeGroupName);



					try
					{
						foreach (var attribute in attributeGroup.Attributes)
						{
							attributeSet.AssignAttribute(attributeGroup.AttributeGroupName, attribute);

						}

					}
					catch
					{

					}


				}

				attributeSet.Save();
			}*/
		}

		[TearDown]
		public void TeardownAttributeSets()
		{
			/*foreach (var attributeSet in attributeSetFixtures)
			{
				try
				{
					var id =
						Client.Search
							.AttributeSets(builder =>
								builder.WhereEquals(set => set.AttributeSetName, attributeSet.AttributeSetName)
									.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct)).Items
							.SingleOrDefault()?
							.AttributeSetId;
					if (id != null)
					{
						Client.AttributeSets.Delete(id.Value);

					}

				}
				catch
				{

				}
			}

			foreach (var fixture in attributeFixtures)
			{
				Client.Attributes.DeleteProductAttribute(fixture.AttributeCode);
			}*/
		}
	}
}