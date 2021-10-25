using System.Threading.Tasks;
using Magento.RestClient.Domain.Models.Catalog;
using Magento.RestClient.Domain.Tests.Abstractions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
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