using System;
using System.IO;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Catalog.Models.Products
{
	public record ProductMediaContent
	{
		public ProductMediaContent(FileInfo fileInfo)
		{
			var bytes = File.ReadAllBytes(fileInfo.FullName);
			this.Base64EncodedData = Convert.ToBase64String(bytes);
			this.Name = fileInfo.Name;
			this.Type = MimeTypes.GetMimeType(fileInfo.Name);
		}

		public ProductMediaContent(string path) : this(new FileInfo(path))
		{
		}

		[JsonProperty("base64_encoded_data")] public string Base64EncodedData { get; set; }

		[JsonProperty("type")] public string Type { get; set; }

		[JsonProperty("name")] public string Name { get; set; }
	}
}