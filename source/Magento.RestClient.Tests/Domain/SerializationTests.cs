using System.Threading.Tasks;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Models.Catalog;
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
}