namespace Magento.RestClient.Search
{
	public static class SearchConditionExtensions
	{
		public static string GetMagentoCondition(this SearchCondition condition)
		{
			return condition switch {
				SearchCondition.Equals => "eq",
				SearchCondition.InSet => "finset",
				SearchCondition.From => "from",
				SearchCondition.GreaterThan => "gt",
				SearchCondition.GreaterThanOrEqual => "gteq",
				SearchCondition.Like => "like",
				SearchCondition.LessThan => "lt",
				SearchCondition.LessThanOrEqual => "lteq",
				SearchCondition.NotEqual => "neq",
				SearchCondition.NotWithinSet => "nfinset",
				SearchCondition.NotIn => "nin",
				SearchCondition.NotNull => "notnull",
				SearchCondition.IsNull => "isnull",
				SearchCondition.To => "to",
				_ => ""
			};
		}
	}
}