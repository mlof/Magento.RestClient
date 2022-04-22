using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Requests;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using Polly;
using RestSharp;
using Serilog;

namespace Magento.RestClient.Data.Repositories
{
	public class AsyncRepository : AbstractRepository, IAsyncRepository
	{
		public AsyncRepository(IContext context) : base(context)
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
			var policy = Policy.Handle<System.Net.WebException>().WaitAndRetry(5, retryAttempt =>
				TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
			);
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
                
				var statusResponse =
					await policy.Execute(async () => await GetStatus(uuid).ConfigureAwait(false));


				Log.Information("{Uuid}:\t({Completed}/{Total})", uuid,
					statusResponse.OperationsList.Count(item => item.Status == OperationStatus.Complete),
					initialResponse.OperationCount);

				if (statusResponse.OperationsList.All(list => list.Status != OperationStatus.Open))
				{
                    foreach (var operationItem in statusResponse.OperationsList)
                    {

                        if (operationItem.Status != OperationStatus.Complete)
                        {
							Log.Warning("Operation {Id} failed: {ErrorMessage}", operationItem.Id, operationItem.ResultMessage);
                        }
                    }
					return statusResponse;
				}


				await Task.Delay(delay.Value).ConfigureAwait(false);
			}


		}

		public IQueryable<BulkOperation> AsQueryable()
		{
			return new MagentoQueryable<BulkOperation>(this.Client, "bulk");
		}


	

	

		
	}
	

	public class ProductAsyncRepository : AbstractRepository, IProductAsyncRepository
	{
		public ProductAsyncRepository(IContext context) : base(context)
		{
		}
		public Task<BulkActionResponse> Post(params Product[] models)
		{
			var request = new RestRequest("products", Method.POST);
			request.SetScope("all/async/bulk");

			request.AddJsonBody(
				models.Select(product => new { product = product }).ToList()
			);

			return ExecuteAsync<BulkActionResponse>(request);
		}
		public Task<BulkActionResponse> PostMediaBySku(
			params CreateOrUpdateMediaRequest[] media)
		{
			var request = new RestRequest("products/bySku/media", Method.POST);
			request.SetScope("all/async/bulk");


			request.AddJsonBody(media.ToList());

			return ExecuteAsync<BulkActionResponse>(request);
		}
	}
}