using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Magento.RestClient.Domain;
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
			var attributes = File.ReadAllText(Path.Join("Fixtures", "attributes.json"));
			var attributeSets = File.ReadAllText(Path.Join("Fixtures", "attribute-sets.json"));
			this.attributeSetFixtures = JsonConvert.DeserializeObject<List<AttributeSetFixture>>(attributeSets);
			this.attributeFixtures = JsonConvert.DeserializeObject<List<AttributeFixture>>(attributes);
		}


		[Test]
		public void CreateFixtures()
		{
			var attributeFactory = new ProductAttributeFactory(Client);

			
			foreach (var fixture in attributeFixtures)
			{
				var attribute = attributeFactory.GetInstance(fixture.AttributeCode);
				attribute.SetFrontendLabel(fixture.FrontendLabel);
				attribute.SetFrontendInput(fixture.FrontendInput);

				if (fixture.Option != null)
				{
					foreach (var option in fixture.Option)
					{
						attribute.AddOption(option);
					}
				}

				attribute.Save();
			}


			var attributeSetFactory = new AttributeSetModelFactory(Client);
			foreach (var fixture in attributeSetFixtures)
			{
				var attributeSet = attributeSetFactory.GetInstance(fixture.Name);
				attributeSet.AddGroup("Fixtures");

				foreach (var attributeCode in fixture.Attributes)
				{

					try
					{
						attributeSet.AssignAttribute("Fixtures", attributeCode);

					}
					catch
					{

					}


				}

				attributeSet.Save();
			}
		}

		[TearDown]
		public void TeardownAttributeSets()
		{
			foreach (var attributeSet in attributeSetFixtures)
			{
				try
				{
					var id =
						Client.Search
							.AttributeSets(builder =>
								builder.WhereEquals(set => set.AttributeSetName, attributeSet.Name)
									.WhereEquals(set => set.EntityTypeId, EntityType.CatalogProduct)).Items
							.SingleOrDefault()
							.AttributeSetId;
					Client.AttributeSets.Delete(id.Value);

				}
				catch
				{

				}
			}

			foreach (var fixture in attributeFixtures)
			{
				Client.Attributes.DeleteProductAttribute(fixture.AttributeCode);
			}
		}
	}

	public class AttributeSetFixture
	{
		[JsonProperty("Name")] public string Name { get; set; }

		[JsonProperty("Attributes")] public List<string> Attributes { get; set; }
	}

	public partial class AttributeFixture
	{
		[JsonProperty("attribute_code")] public string AttributeCode { get; set; }

		[JsonProperty("frontend_input")] public string FrontendInput { get; set; }

		[JsonProperty("frontend_label")] public string FrontendLabel { get; set; }

		[JsonProperty("option", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Option { get; set; }
	}


	public class AttributeTests : AbstractIntegrationTest
	{
	}
}