using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Tests.Domain.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class AttributeSetTests : AbstractDomainObjectTest
	{
		[SetUp]
		public void SetupAttributeSets()
		{
			var attributeSet = Context.GetAttributeSetModel(this.ExistingAttributeSet);
			attributeSet.Save();
		}

		public string ExistingAttributeSet { get; set; } = "SHOULD EXIST";


		[Test]
		public void GetAttributeSetModel_Existing()
		{
			var attributeSet =  Context.GetAttributeSetModel(this.ExistingAttributeSet);

			attributeSet.IsPersisted.Should().BeTrue();
		}
		[Test]
		public void GetAttributeSetModel_DoesNotExist()
		{
			var attributeSet = Context.GetAttributeSetModel("DOESNOTEXIST");

			
			attributeSet.IsPersisted.Should().BeFalse();
		}

		[Test]
		public void Add()
		{

			var attributeSet =  Context.GetAttributeSetModel("Test Attribute Set");

			attributeSet.AddGroup("Test Group");





			attributeSet.Save();
			
		}


		[TearDown]
		public void TeardownAttributeSets()
		{
			var testNames = new List<string>() {this.ExistingAttributeSet};

			foreach (var name in testNames)
			{
				var attributeSet = Context.AttributeSets.SingleOrDefault(set => set.AttributeSetName == name);

				Context.AttributeSets.Delete(attributeSet.AttributeSetId.Value);
			}



		}
	}
}