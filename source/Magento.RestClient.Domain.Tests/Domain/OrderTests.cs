using Magento.RestClient.Domain.Models.Orders;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
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