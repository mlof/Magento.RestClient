using System.Collections.Generic;
using System.IO;
using System.Linq;
using Magento.RestClient.Domain.Tests.Abstractions;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Products;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class AttributeSetTests : AbstractDomainObjectTest
	{
		[SetUp]
		public void SetupAttributeSets()
		{
		}


		[Test]
		public void CreateNew()
		{
			var factory = new AttributeSetModelFactory(Client);
			var attributeSet = factory.GetInstance("Test Attribute Set");


			attributeSet.AddGroup("");

			attributeSet.Save();
		}


		[TearDown]
		public void TeardownAttributeSets()
		{
			var attributeSet = Client.Search.AttributeSets(builder =>
				builder.WhereEquals(set => set.AttributeSetName, "Test Attribute Set")).Items.SingleOrDefault();
			Client.AttributeSets.Delete(attributeSet.AttributeSetId.Value);
		}
	}
}