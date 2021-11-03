using System.Threading.Tasks;
using Magento.RestClient.Domain.Models.Orders;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class OrderTests : AbstractAdminTest
	{


		[Test]
		async public Task GetExistingOrder()
		{
			var order = new OrderModel(Context, 13);

			order.Status = "processing";
			await order.SaveAsync();

		}
		 
	}
}