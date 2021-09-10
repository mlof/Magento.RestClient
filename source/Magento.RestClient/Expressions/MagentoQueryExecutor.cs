using System;
using System.Collections.Generic;
using System.Linq;
using Magento.RestClient.Data.Models.Search;
using Magento.RestClient.Expressions.QueryGeneration;
using Remotion.Linq;
using RestSharp;

namespace Magento.RestClient.Expressions
{
	public class MagentoQueryExecutor : IQueryExecutor
	{
		private readonly IRestClient _client;
		private readonly RestRequest _restRequest;

		public MagentoQueryExecutor(IRestClient client, string resource)
		{
			this._client = client;
			_restRequest = new RestRequest(resource);
		}


		public T ExecuteScalar<T>(QueryModel queryModel)
		{
			return ExecuteCollection<T>(queryModel).Single();
		}


		public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
		{
			return returnDefaultWhenEmpty
				? ExecuteCollection<T>(queryModel).SingleOrDefault()
				: ExecuteCollection<T>(queryModel).Single();
		}

		public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
		{
			var visitor = new QueryModelVisitor();
			visitor.VisitQueryModel(queryModel);
			var r = visitor.GetRequest(_restRequest);

			var result = 
			_client.Execute<SearchResponse<T>>(r);

			if (result.IsSuccessful)
			{
				return result.Data.Items;

			}
			else
			{
				throw new Exception();
			}
		}
	}
}