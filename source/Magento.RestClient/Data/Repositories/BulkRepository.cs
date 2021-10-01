using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using RestSharp;

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
			if (delay == null)
			{
				delay = TimeSpan.FromSeconds(5);
			}

			while (true)
			{
				var status = await GetStatus(uuid).ConfigureAwait(false);
				if (status.OperationsList.All(list => list.Status != OperationStatus.Open))
				{
					return status;
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