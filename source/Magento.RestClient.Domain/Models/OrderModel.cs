using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Domain.Abstractions;
using Magento.RestClient.Models.Shipping;
using Magento.RestClient.Repositories;
using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain.Models
{
	public class OrderModel :  IOrderModel
	{
		private RestClient.Models.Orders.Order _model;
		private readonly IAdminClient _client;
		private List<Invoice> _invoices;
		private List<Shipment> _shipments;
		public long OrderId { get; private init; }

		public bool IsInvoiced => _invoices.Any();
		private OrderModel(IAdminClient client, long orderId)
		{
			_client = client;
			this.OrderId = orderId;
			Refresh();
		}

	
		public OrderModel CreateInvoice()
		{
			if (!this.IsInvoiced)
			{
				_client.Orders.CreateInvoice(this.OrderId);

			}
			return this;

		}

		public bool IsPersisted { get; }

		public void Refresh()
		{
			this._model = _client.Orders.GetByOrderId(this.OrderId);
			this._invoices = _client.Invoices.GetByOrderId(this.OrderId);
			this._shipments = _client.Shipments.GetByOrderId(this.OrderId);
		}

		public void Save()
		{

		}

		public static OrderModel GetExisting(IAdminClient client, long orderId)
		{
			return new OrderModel(client, orderId);
		}

		public OrderModel CreateShipment()
		{
			_client.Shipments.CreateShipment(this.OrderId);
			Refresh();
			return this;
		}
	}
}