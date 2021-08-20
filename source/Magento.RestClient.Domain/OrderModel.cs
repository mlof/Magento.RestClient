using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class OrderModel
	{
		private readonly IOrderRepository _orderRepository;
		private Models.Orders.Order _model;
		public long OrderId { get; private init; }

		private OrderModel(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public static OrderModel GetExisting(IOrderRepository orderRepository, long orderId)
		{
			var order = new OrderModel(orderRepository) {OrderId = orderId};


			return order.UpdateMagentoValues();
		}

		public OrderModel UpdateMagentoValues()
		{
			this._model = _orderRepository.GetOrderById(this.OrderId);
			return this;
		}

		public OrderModel CreateInvoice()
		{
			_orderRepository.CreateInvoice(this.OrderId);
			return UpdateMagentoValues();
		}
	}
}