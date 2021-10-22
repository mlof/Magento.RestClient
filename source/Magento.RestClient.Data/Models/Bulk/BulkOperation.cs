using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Bulk
{
	public partial class BulkOperation
	{
		[JsonProperty("operations_list")] public List<OperationItem> OperationsList { get; set; }

		[JsonProperty("user_type")] public UserType UserType { get; set; }

		[JsonProperty("bulk_id")] public Guid BulkId { get; set; }

		[JsonProperty("description")] public string Description { get; set; }

		[JsonProperty("start_time")] public DateTimeOffset StartTime { get; set; }

		[JsonProperty("user_id")] public long UserId { get; set; }

		[JsonProperty("operation_count")] public long OperationCount { get; set; }
	}
}