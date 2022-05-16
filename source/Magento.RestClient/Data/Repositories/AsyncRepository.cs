using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Bulk;
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
			var request = new RestRequest("bulk/{uuid}/status");
			request.AddParameter("uuid", uuid, ParameterType.UrlSegment);
			request.SetScope("all");

			return ExecuteAsync<BulkOperation>(request);
		}

		async public Task<BulkOperation> AwaitBulkOperations(Guid uuid, TimeSpan? delay = null)
		{
			var logger = this.Logger.ForContext("BulkUuid", uuid);
			var policy = Policy.Handle<WebException>().WaitAndRetry(5, retryAttempt =>
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
							Log.Warning("Operation {Id} failed: {ErrorMessage}", operationItem.Id,
								operationItem.ResultMessage);
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
}