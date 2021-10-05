using System.Threading.Tasks;
using FluentValidation;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Tests.Domain.Abstractions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{

	public class SerializationTests : AbstractDomainObjectTest
	{

		[Test]
		async public Task CanSerializeProductModel()
		{
			var model = new ProductModel(Context, "522728");


			var json = JsonConvert.SerializeObject(model, Formatting.Indented);

		}

		[Test]
		async public Task CanDeserializeProductModel()
		{



		}

	}
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