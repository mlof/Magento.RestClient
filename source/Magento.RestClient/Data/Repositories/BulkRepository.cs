using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
		private readonly IRestClient client;


		public BulkRepository(IRestClient client)
		{
			this.client = client;
		}


		public async Task<BulkOperation> GetStatus(Guid uuid)
		{
			var request = new RestRequest("bulk/{uuid}/status");
			request.Method = Method.GET;
			request.AddParameter("uuid", uuid, ParameterType.UrlSegment);
			request.SetScope("all");


			var response = await client.ExecuteAsync<BulkOperation>(request);
			return response.Data;
		}


		public async Task<BulkOperation> Await(Guid uuid, TimeSpan? delay = null)
		{
			if (delay == null)
			{
				delay = TimeSpan.FromSeconds(5);
			}

			while (true)
			{
				var status = await GetStatus(uuid);
				if (status.OperationsList.All(list => list.Status != OperationStatus.Open))
				{
					return status;
				}

				await Task.Delay(delay.Value);
			}
		}


		public IQueryable<BulkOperation> AsQueryable()
		{
			return new MagentoQueryable<BulkOperation>(client, "bulk");
		}
	}
}