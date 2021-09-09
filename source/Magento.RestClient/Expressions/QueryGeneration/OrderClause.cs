using Remotion.Linq.Clauses;

namespace Magento.RestClient.Expressions.QueryGeneration
{
	public class OrderClause
	{
		public OrderingDirection Direction { get; set; }
		public string PropertyName { get; set; }
	}
}