using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using RestSharp;

namespace Magento.RestClient.Expressions
{
	public class MagentoQueryable<T> : QueryableBase<T>
	{
		public MagentoQueryable(IQueryParser queryParser, IQueryExecutor executor) : base(queryParser, executor)
		{
		}

		public MagentoQueryable(IQueryProvider provider) : base(provider)
		{
		}

		public MagentoQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
		{
		}

		public MagentoQueryable(IRestClient restClient, string resource) : base(
			new DefaultQueryProvider(typeof(MagentoQueryable<>),
				QueryParser.CreateDefault(), new MagentoQueryExecutor(restClient, resource)))
		{
		}
	}

	public static class QueryableExtensions
	{
		public static Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable)
		{

			return Task.FromResult(queryable.ToList());
		}
	}
}