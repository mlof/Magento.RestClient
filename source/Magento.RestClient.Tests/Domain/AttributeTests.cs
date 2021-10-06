using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Models.EAV;
using Magento.RestClient.Tests.Domain.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class AttributeTests : AbstractDomainObjectTest
	{
		[Test]
		async public Task Create_WithoutLabel_ShouldThrowValidationException()
		{
			var attribute = new AttributeModel(Context, "testattribute");

			Assert.ThrowsAsync<ValidationException>(() =>
				attribute.SaveAsync()
			);
		}
		[Test]
		async public Task Create_IllegalCharactersInCode_ShouldThrowValidationException()
		{
			var attribute = new AttributeModel(Context, "testattribute-IllegalCharacter");

			Assert.ThrowsAsync<ValidationException>(() =>
				attribute.SaveAsync()
			);
		}
		[Test]
		async public Task Create()
		{
			var attribute = new AttributeModel(Context, "attribute-with-label");
			attribute.DefaultFrontendLabel = "Attribute";


			await attribute.SaveAsync();
		}
	}
}