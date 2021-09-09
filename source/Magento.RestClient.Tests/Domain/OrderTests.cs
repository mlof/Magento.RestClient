using Magento.RestClient.Domain.Models;
using Magento.RestClient.Tests.Domain.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	class OrderTests : AbstractDomainObjectTest
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