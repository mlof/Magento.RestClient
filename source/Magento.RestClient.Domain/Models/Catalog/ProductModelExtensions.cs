using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Domain;
using Magento.RestClient.Data.Models.Catalog.Products;
using Serilog;

namespace Magento.RestClient.Domain.Models.Catalog
{
	public static class ProductModelExtensions
	{
		public static bool ContainsFileName(this IEnumerable<MediaEntry> entries, string
			filename)
		{
			return entries.Any(media => media.Label != filename);
		}

		public static async Task AddMediaEntryFromUri(this IProductModel productModel, Uri requestUri)
		{
			var filename = System.IO.Path.GetFileName(requestUri.AbsolutePath);

			if (!productModel.MediaEntries.ContainsFileName(filename))
			{
				var directoryPath = Path.Join(Path.GetTempPath(), "Magento.RestClient", "Products", productModel.Sku,
					"Images");
				Directory.CreateDirectory(directoryPath);
				var filePath = Path.Join(Path.GetTempPath(), "Magento.RestClient", "Products", productModel.Sku,
					"Images",
					filename);
				Log.Information("Downloading {Filename} to {FilePath}", filename, filePath);

				using var httpClient = new WebClient();
				await httpClient.DownloadFileTaskAsync(requestUri, filePath).ConfigureAwait(false);
				productModel.AddMediaEntry(filePath);
			}
		}

		public static void AddMediaEntry(this IProductModel productModel, FileInfo fileInfo)
		{
			if (!productModel.MediaEntries.ContainsFileName(fileInfo.Name))
			{
				productModel.AddMediaEntry(
					new MediaEntry
					{
						Label = fileInfo.Name,
						Disabled = false,
						MediaType = ProductMediaType.Image,
						Content = new ProductMediaContent(fileInfo)
					});
			}
		}

		public static void AddMediaEntry(this IProductModel productModel, string fileName)
		{
			var fileInfo = new FileInfo(fileName);

			productModel.AddMediaEntry(fileInfo);
		}
	}
}