using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.AsynchronousOperations.Models
{
	public class BulkActionResponse
	{
		[JsonProperty("bulk_uuid")] public Guid BulkUuid { get; set; }

		[JsonProperty("request_items")] public List<RequestItem> RequestItems { get; set; }

		[JsonProperty("errors")] public bool Errors { get; set; }
	}
}