using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Domain.Models.EAV;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Domain.Tests.Abstractions
{
	public abstract class AbstractAdminTest
	{
		protected IAdminContext Context;

		public long AttributeSetId { get; set; }

		[OneTimeSetUp]
		public async Task SetupFixtures()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

			var configurationRoot = new ConfigurationBuilder().AddUserSecrets<MagentoClientOptions>().Build();

			var configuration = new MagentoClientOptions();
			configurationRoot.Bind(configuration);

			this.Context = new MagentoAdminContext(configuration);
			var configurableAxisModel = new AttributeModel(Context, "test_configurable_axis") {
				DefaultFrontendLabel = "Test Configurable Axis", FrontendInput = AttributeFrontendInput.Select
			};
			configurableAxisModel.AddOption("X");
			configurableAxisModel.AddOption("Y");
			configurableAxisModel.AddOption("Z");


			var attributeSet = new AttributeSetModel(Context, "Unit Test Attribute Set", EntityType.CatalogProduct);
			attributeSet["Test Attributes"].AssignAttributes(configurableAxisModel);
			await attributeSet.SaveAsync().ConfigureAwait(false);

			this.AttributeSetId = attributeSet.Id;
		}

		[OneTimeTearDown]
		async public Task TeardownFixtures()
		{
			await Context.Attributes.DeleteProductAttribute("test_configurable_axis").ConfigureAwait(false);
			var attributeSetId = Context.AttributeSets
				.AsQueryable().Single(set => set.AttributeSetName == "Unit Test Attribute Set").AttributeSetId;
			if (attributeSetId != null)
			{
				await Context.AttributeSets.Delete(attributeSetId.Value)
					.ConfigureAwait(false);
			}
		}


		protected void DeleteProductIfExists(string sku)
		{
			var exists = Context.Products.GetProductBySku(sku) != null;

			if (exists)
			{
				Context.Products.DeleteProduct(sku);
			}
		}

	}
}