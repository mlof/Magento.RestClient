using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magento.RestClient.Expressions
{
	public static class QueryableExtensions
	{
		public static Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable)
		{
			return Task.FromResult(queryable.ToList());
		}
	}
}