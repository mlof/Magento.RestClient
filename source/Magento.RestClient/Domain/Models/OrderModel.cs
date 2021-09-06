using System;
using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Orders;
using Magento.RestClient.Data.Models.Shipping;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Domain.Models
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
			Refresh();
		}

		public long OrderId { get; }

		public bool IsInvoiced => _invoices.Any();

		public bool IsPersisted { get; }

		public void Refresh()
		{
			_model = _context.Orders.GetByOrderId(this.OrderId);
			_invoices = _context.Invoices.GetByOrderId(this.OrderId);
			_shipments = _context.Shipments.GetByOrderId(this.OrderId);
		}

		public void Save()
		{
		}

		public void Delete()
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

		public OrderModel CreateShipment()
		{
			_context.Shipments.CreateShipment(this.OrderId);
			Refresh();
			return this;
		}
	}
}