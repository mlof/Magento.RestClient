using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Stock
{
	public record StockItem
	{
		[JsonProperty("item_id")] public long? ItemId { get; set; }

		[JsonProperty("product_id")] public long? ProductId { get; set; }

		[JsonProperty("stock_id")] public long? StockId { get; set; }

		[JsonProperty("qty")] public long? Qty { get; set; }

		[JsonProperty("is_in_stock")] public bool? IsInStock { get; set; }

		[JsonProperty("is_qty_decimal")] public bool? IsQtyDecimal { get; set; }

		[JsonProperty("show_default_notification_message")]
		public bool? ShowDefaultNotificationMessage { get; set; }

		[JsonProperty("use_config_min_qty")] public bool? UseConfigMinQty { get; set; }

		[JsonProperty("min_qty")] public long? MinQty { get; set; }

		[JsonProperty("use_config_min_sale_qty")]
		public long? UseConfigMinSaleQty { get; set; }

		[JsonProperty("min_sale_qty")] public long? MinSaleQty { get; set; }

		[JsonProperty("use_config_max_sale_qty")]
		public bool? UseConfigMaxSaleQty { get; set; }

		[JsonProperty("max_sale_qty")] public long? MaxSaleQty { get; set; }

		[JsonProperty("use_config_backorders")]
		public bool? UseConfigBackorders { get; set; }

		[JsonProperty("backorders")] public long? Backorders { get; set; }

		[JsonProperty("use_config_notify_stock_qty")]
		public bool? UseConfigNotifyStockQty { get; set; }

		[JsonProperty("notify_stock_qty")] public long? NotifyStockQty { get; set; }

		[JsonProperty("use_config_qty_increments")]
		public bool? UseConfigQtyIncrements { get; set; }

		[JsonProperty("qty_increments")] public long? QtyIncrements { get; set; }

		[JsonProperty("use_config_enable_qty_inc")]
		public bool? UseConfigEnableQtyInc { get; set; }

		[JsonProperty("enable_qty_increments")]
		public bool? EnableQtyIncrements { get; set; }

		[JsonProperty("use_config_manage_stock")]
		public bool? UseConfigManageStock { get; set; }

		[JsonProperty("manage_stock")] public bool? ManageStock { get; set; }

		[JsonProperty("low_stock_date")] public object LowStockDate { get; set; }

		[JsonProperty("is_decimal_divided")] public bool? IsDecimalDivided { get; set; }

		[JsonProperty("stock_status_changed_auto")]
		public long? StockStatusChangedAuto { get; set; }
	}
}