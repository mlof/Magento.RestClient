using Newtonsoft.Json;

namespace Magento.RestClient.Models.Common
{
	public partial class Currency
	{
		[JsonProperty("global_currency_code")] public string GlobalCurrencyCode { get; set; }

		[JsonProperty("base_currency_code")] public string BaseCurrencyCode { get; set; }

		[JsonProperty("store_currency_code")] public string StoreCurrencyCode { get; set; }

		[JsonProperty("quote_currency_code")] public string QuoteCurrencyCode { get; set; }

		[JsonProperty("store_to_base_rate")] public long StoreToBaseRate { get; set; }

		[JsonProperty("store_to_quote_rate")] public long StoreToQuoteRate { get; set; }

		[JsonProperty("base_to_global_rate")] public long BaseToGlobalRate { get; set; }

		[JsonProperty("base_to_quote_rate")] public long BaseToQuoteRate { get; set; }
	}
}