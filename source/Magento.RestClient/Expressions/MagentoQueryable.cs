using Microsoft.Extensions.Caching.Memory;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        public MagentoQueryable(RestSharp.RestClient restClient, string resource, IMemoryCache cache = null,
            TimeSpan? relativeExpiration = null) : base(
            new DefaultQueryProvider(typeof(MagentoQueryable<>),
                QueryParser.CreateDefault(), new MagentoQueryExecutor(restClient, resource, cache, relativeExpiration)))
        {
        }
    }
}