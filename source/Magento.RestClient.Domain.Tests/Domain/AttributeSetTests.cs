using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Domain.Models.EAV;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class AttributeSetTests : AbstractAdminTest
	{
		[Test]
		async public Task CreateCompleteAttributeSet()
		{


			


			var attributeSet = new AttributeSetModel(Context, "Monitors", EntityType.CatalogProduct) { };

			attributeSet["Panel"].AssignAttributes(
				new AttributeModel(Context, "monitor_resolution") {
					DefaultFrontendLabel = "Resolution",
					FrontendInput = AttributeFrontendInput.Select,
					Options = new List<string>() {"1366x768", "1920x1080", "2560x1080", "2560x1440"}
				});

			attributeSet["Connections"].AssignAttributes(
				new AttributeModel(Context, "monitor_has_dvi") {
					DefaultFrontendLabel = "Has DVI",
					FrontendInput = AttributeFrontendInput.Select,
					Options = new List<string>() {"Yes", "No"}
				});

			await attributeSet.SaveAsync();
		}

		[TearDown]
		async public Task TeardownAttributeSets()
		{
			var set = Context.AttributeSets.AsQueryable().Where(set => set.AttributeSetName == "Monitors")
				.SingleOrDefault();
			var model = new AttributeSetModel(Context, set.AttributeSetId.Value);
			await model.Delete();
		}
	}
}