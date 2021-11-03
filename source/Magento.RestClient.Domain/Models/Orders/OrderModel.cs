using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Domain;
using Magento.RestClient.Data.Models.Invoices;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Data.Models.Shipping;

namespace Magento.RestClient.Domain.Models.Orders
{
	public class OrderModel : IOrderModel
	{
		private readonly IAdminContext _context;
		private List<Invoice> _invoices;
		private List<Shipment> _shipments;
		public string? Status { get; set; }

		public OrderModel(IAdminContext context, long orderId)
		{
			_context = context;
			this.OrderId = orderId;
			Refresh().GetAwaiter().GetResult();
		}

		public long OrderId { get; }

		public bool IsInvoiced => _invoices.Any();

		public bool IsPersisted { get; private set; }

		public async Task Refresh()
		{

			var model = await _context.Orders.GetByOrderId(this.OrderId).ConfigureAwait(false);
			if (model != null)
			{
				this.IsPersisted = true;
			}
			Mapper.Map(model).Over(this);
			
			_invoices = await _context.Invoices.GetByOrderId(this.OrderId).ConfigureAwait(false);
			_shipments = _context.Shipments.GetByOrderId(this.OrderId);
		}

		async public Task SaveAsync()
		{
			var model = new Order() { EntityId =  OrderId, Status =  this.Status};
			await _context.Orders.CreateOrder(model).ConfigureAwait(false);
		}

		public Task Delete()
		{
			// don't get me started
			throw new NotSupportedException();
		}

		public async Task CreateInvoice()
		{
			if (!this.IsInvoiced)
			{
				await _context.Orders.CreateInvoice(this.OrderId).ConfigureAwait(false);
			}
		}

	

		public async Task CreateShipment()
		{
			await _context.Shipments.CreateShipment(this.OrderId).ConfigureAwait(false);
			await Refresh().ConfigureAwait(false);
		}
	}
}