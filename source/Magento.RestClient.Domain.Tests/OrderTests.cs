using Magento.RestClient.Domain.Models.Orders;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class OrderTests : AbstractAdminTest
	{

		[Test]
		public void CreateShipment()
		{
			var order = OrderModel.GetExisting(Context, 35);
			
			order.CreateInvoice();

			order.CreateShipment();

		}
		 
	}
}