using System.Linq;
using System.Linq.Expressions;
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
}