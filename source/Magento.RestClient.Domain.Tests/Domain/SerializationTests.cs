using System.Threading.Tasks;
using Magento.RestClient.Domain.Models.Catalog;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class SerializationTests : AbstractAdminTest
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
}