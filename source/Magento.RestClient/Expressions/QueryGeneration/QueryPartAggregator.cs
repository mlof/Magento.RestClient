using System.Collections.Generic;
using System.Linq;

namespace Magento.RestClient.Expressions.QueryGeneration
{
	public class QueryPartAggregator
	{
		public int PageSize { get; set; } = 0;
		public IList<IList<Filter>> Filtergroups { get; set; } = new List<IList<Filter>> {new List<Filter>()};

		public List<OrderClause> Orderings { get; set; } = new();

		public void CreateNewFilterGroup()
		{
			this.Filtergroups.Add(new List<Filter>());
		}

		public void AddToLastFilterGroup(Filter filter)
		{
			this.Filtergroups.Last().Add(filter);
		}
	}
}