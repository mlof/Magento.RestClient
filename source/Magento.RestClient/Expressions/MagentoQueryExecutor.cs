using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Search;
using Magento.RestClient.Expressions.QueryGeneration;
using Microsoft.Extensions.Caching.Memory;
using Remotion.Linq;
using RestSharp;

namespace Magento.RestClient.Expressions
{
	public class MagentoQueryExecutor : IQueryExecutor
	{
		private readonly IRestClient _client;
		private readonly RestRequest _restRequest;
		private readonly IMemoryCache _cache;
		private readonly TimeSpan? _relativeExpiration;
		private readonly string _resource;

		public MagentoQueryExecutor(IRestClient client, string resource, IMemoryCache cache,
			TimeSpan? relativeExpiration = null)
		{
			_client = client;
			_relativeExpiration = relativeExpiration ?? (TimeSpan?)TimeSpan.FromSeconds(5);

			_resource = resource;
			_restRequest = new RestRequest(resource);
			_cache = cache ?? new MemoryCache(new MemoryCacheOptions());
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
			var uriKey = _client.BuildUri(r).ToString().CreateMd5();
			var key = $"{nameof(MagentoQueryExecutor)}.{typeof(T).Name}.{uriKey}";

			return _cache.GetOrCreate(key, entry => {
				entry.AbsoluteExpirationRelativeToNow = _relativeExpiration;
				var result =
					_client.Execute<SearchResponse<T>>(r);

				if (result.IsSuccessful)
				{
					if (result.Data != null)
					{
						return result.Data.Items;
					}
					else
					{
						return new List<T>();
					}
				}
				else
				{
					throw new Exception();
				}
			});
		}
	}
}