using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Invoices;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Data.Models.Shipping;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models.Orders
{
	public class OrderModel : IOrderModel
	{
		private readonly IAdminContext _context;
		private List<Invoice> _invoices;
		private Order _model;
		private List<Shipment> _shipments;

		private OrderModel(IAdminContext context, long orderId)
		{
			_context = context;
			this.OrderId = orderId;
			Refresh().GetAwaiter().GetResult();
		}

		public long OrderId { get; }

		public bool IsInvoiced => _invoices.Any();

		public bool IsPersisted { get; }

		public async Task Refresh()
		{
			_model = await _context.Orders.GetByOrderId(this.OrderId).ConfigureAwait(false);
			_invoices = await _context.Invoices.GetByOrderId(this.OrderId).ConfigureAwait(false);
			_shipments = _context.Shipments.GetByOrderId(this.OrderId);
		}

		public async Task SaveAsync()
		{
		}

		public async Task Delete()
		{
			// don't get me started
			throw new NotSupportedException();
		}

		public OrderModel CreateInvoice()
		{
			if (!this.IsInvoiced)
			{
				_context.Orders.CreateInvoice(this.OrderId);
			}

			return this;
		}

		public static OrderModel GetExisting(IAdminContext context, long orderId)
		{
			return new(context, orderId);
		}

		public async Task<OrderModel> CreateShipment()
		{
			await _context.Shipments.CreateShipment(this.OrderId).ConfigureAwait(false);
			await Refresh().ConfigureAwait(false);
			return this;
		}
	}
}