using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Models.Orders;
using Magento.RestClient.Models.Shipping;
using Magento.RestClient.Repositories;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class OrderModel : IOrderModel
	{
		private readonly IAdminClient _client;
		private List<Invoice> _invoices;
		private Order _model;
		private List<Shipment> _shipments;

		private OrderModel(IAdminClient client, long orderId)
		{
			_client = client;
			this.OrderId = orderId;
			Refresh();
		}

		public long OrderId { get; }

		public bool IsInvoiced => _invoices.Any();

		public bool IsPersisted { get; }

		public void Refresh()
		{
			_model = _client.Orders.GetByOrderId(this.OrderId);
			_invoices = _client.Invoices.GetByOrderId(this.OrderId);
			_shipments = _client.Shipments.GetByOrderId(this.OrderId);
		}

		public void Save()
		{
		}


		public OrderModel CreateInvoice()
		{
			if (!this.IsInvoiced)
			{
				_client.Orders.CreateInvoice(this.OrderId);
			}

			return this;
		}

		public static OrderModel GetExisting(IAdminClient client, long orderId)
		{
			return new(client, orderId);
		}

		public OrderModel CreateShipment()
		{
			_client.Shipments.CreateShipment(this.OrderId);
			Refresh();
			return this;
		}
	}
}