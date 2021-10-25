using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Domain.Models.EAV;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class AttributeSetTests : AbstractAdminTest
	{
		[Test]
		async public Task CreateCompleteAttributeSet()
		{



			var configurableAxisModel = new AttributeModel(Context, "test_configurable_axis")
			{
				DefaultFrontendLabel = "Test Configurable Axis",
				FrontendInput = AttributeFrontendInput.Select
			};
			configurableAxisModel.AddOption("X");
			configurableAxisModel.AddOption("Y");
			configurableAxisModel.AddOption("Z");

			await configurableAxisModel.SaveAsync();


			var attributeSet = new AttributeSetModel(Context, "Unit Test Attribute Set", EntityType.CatalogProduct);
			attributeSet["Test Attributes"].AssignAttributes(configurableAxisModel);
			await attributeSet.SaveAsync();
		}

		[TearDown]
		async public Task TeardownAttributeSets()
		{
			var set = Context.AttributeSets.AsQueryable().Where(set => set.AttributeSetName == "Unit Test Attribute Set")
				.SingleOrDefault();
			var model = new AttributeSetModel(Context, set.AttributeSetId.Value);
			await model.Delete();
		}
	}
}