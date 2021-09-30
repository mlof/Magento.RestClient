using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Tests.Domain.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class AttributeSetTests : AbstractDomainObjectTest
	{
		[SetUp]
		async public Task SetupAttributeSets()
		{
			var attributeSet = Context.GetAttributeSetModel(this.ExistingAttributeSet);
			await attributeSet.SaveAsync();
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
		async public Task Add()
		{

			var attributeSet =  Context.GetAttributeSetModel("Test Attribute Set");

			attributeSet.AddGroup("Test Group");





			await attributeSet.SaveAsync();
			
		}


		[TearDown]
		public void TeardownAttributeSets()
		{
			var testNames = new List<string>() {this.ExistingAttributeSet};

			foreach (var name in testNames)
			{
				var attributeSet = Context.AttributeSets.AsQueryable().SingleOrDefault(set => set.AttributeSetName == name);

				Context.AttributeSets.Delete(attributeSet.AttributeSetId.Value);
			}



		}
	}
}