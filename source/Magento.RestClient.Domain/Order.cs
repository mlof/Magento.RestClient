using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class Order
	{
		private readonly IOrderRepository _orderRepository;
		private Models.Orders.Order _model;
		public long OrderId { get; private init; }

		private Order(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public static Order GetExisting(IOrderRepository orderRepository, long orderId)
		{
			var order = new Order(orderRepository) {OrderId = orderId};


			return order.UpdateMagentoValues();
		}

		public Order UpdateMagentoValues()
		{
			this._model = _orderRepository.GetOrderById(this.OrderId);
			return this;
		}

		public Order CreateInvoice()
		{
			_orderRepository.CreateInvoice(this.OrderId);
			return UpdateMagentoValues();
		}
	}
}