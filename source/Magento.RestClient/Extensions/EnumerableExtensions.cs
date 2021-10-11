using System.Collections.Generic;
using System.Linq;

namespace Magento.RestClient.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
		{
			return source
				.Select((x, i) => new { Index = i, Value = x })
				.GroupBy(x => x.Index / chunkSize)
				.Select(x => x.Select(v => v.Value).ToList())
				.ToList();
		}

		public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
		{
			return source.Select((item, index) => (item, index));
		}
	}
}