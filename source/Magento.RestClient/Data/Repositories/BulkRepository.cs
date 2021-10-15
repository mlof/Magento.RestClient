using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Data.Repositories.Requests;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using RestSharp;
using Serilog;

namespace Magento.RestClient.Data.Repositories
{
	public class BulkRepository : AbstractRepository, IBulkRepository
	{
		public BulkRepository(IContext context) : base(context)
		{
		}

		public Task<BulkOperation> GetStatus(Guid uuid)
		{
			var request = new RestRequest("bulk/{uuid}/status", Method.GET);
			request.AddParameter("uuid", uuid, ParameterType.UrlSegment);
			request.SetScope("all");

			return ExecuteAsync<BulkOperation>(request);
		}

		public async Task<BulkOperation> AwaitBulkOperations(Guid uuid, TimeSpan? delay = null)
		{
			var logger = Logger.ForContext("BulkUuid", uuid);

			if (delay == null)
			{
				delay = TimeSpan.FromSeconds(5);
			}

			var initialResponse = await GetStatus(uuid).ConfigureAwait(false);
			logger.Information("{BulkUuid}\n" +
			                   "Description:\t{Description}\n" +
			                   "OperationCount:\t{OperationCount}\n" +
			                   "Start Time:\t{StartTime}",
				uuid, initialResponse.Description, initialResponse.OperationCount, initialResponse.StartTime);

			while (true)
			{
				var statusResponse = await GetStatus(uuid).ConfigureAwait(false);


				Log.Information("{Uuid}:\t({Completed}/{Total})", uuid,
					statusResponse.OperationsList.Count(item => item.Status == OperationStatus.Complete),
					initialResponse.OperationCount);

				if (statusResponse.OperationsList.All(list => list.Status != OperationStatus.Open))
				{
					return statusResponse;
				}

				await Task.Delay(delay.Value).ConfigureAwait(false);
			}
		}

		public IQueryable<BulkOperation> AsQueryable()
		{
			return new MagentoQueryable<BulkOperation>(this.Client, "bulk");
		}

		public Task<BulkActionResponse> CreateOrUpdateProducts(params Product[] models)
		{
			var request = new RestRequest("products", Method.POST);
			request.SetScope("all/async/bulk");

			request.AddJsonBody(
				models.Select(product => new {product = product}).ToList()
			);

			return ExecuteAsync<BulkActionResponse>(request);
		}

		public Task<BulkActionResponse> CreateOrUpdateConfigurableOptions(
			params ConfigurableProductOptionRequest[] requests)
		{
			var request = new RestRequest("configurable-products/bySku/options", Method.POST);
			request.SetScope("all/async/bulk");


			request.AddJsonBody(requests.ToList());

			return ExecuteAsync<BulkActionResponse>(request);
		}


		public Task<BulkActionResponse> CreateOrUpdateAttributes(params ProductAttribute[] attributes)
		{
			var maxOptionsPerRequest = 15;

			var request = new RestRequest("products/attributes/byAttributeCode", Method.PUT);
			request.SetScope("all/async/bulk");

			var requests = new List<CreateOrUpdateAttributeRequest>();

			foreach (var attribute in attributes)
			{
				if (attribute.Options == null || attribute.Options.Count <= maxOptionsPerRequest)
				{
					requests.Add(
						new CreateOrUpdateAttributeRequest {
							AttributeCode = attribute.AttributeCode, Attribute = attribute
						});
				}
				else
				{
					foreach (var options in attribute.Options.Chunk(maxOptionsPerRequest))
					{
						requests.Add(
							new CreateOrUpdateAttributeRequest {
								AttributeCode = attribute.AttributeCode,
								Attribute = attribute with {Options = options.ToList()}
							});
					}
				}
			}

			request.AddJsonBody(requests);

			return ExecuteAsync<BulkActionResponse>(request);
		}

		public Task<BulkActionResponse> CreateOrUpdateConfigurations(
			params CreateOrUpdateConfigurationRequest[] configurations)
		{
			var request = new RestRequest("configurable-products/bySku/child", Method.POST);
			request.SetScope("all/async/bulk");


			request.AddJsonBody(configurations.ToList());

			return ExecuteAsync<BulkActionResponse>(request);
		}

		public Task<BulkActionResponse> AssignProductsByCategoryId(long categoryId, params string[] skus)
		{
			throw new NotImplementedException();
		}

		public Task<BulkActionResponse> CreateOrUpdateMedia(
			params CreateOrUpdateMediaRequest[] media)
		{
			var request = new RestRequest("products/bySku/media", Method.POST);
			request.SetScope("all/async/bulk");


			request.AddJsonBody(media.ToList());

			return ExecuteAsync<BulkActionResponse>(request);
		}
	}
}