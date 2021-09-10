﻿namespace Magento.RestClient.Search
{
	public enum SearchCondition
	{
		Equals, // Equals
		InSet, // A value within a set of values
		From, // The beginning of a range. Must be used with to.
		GreaterThan, // Greater than
		GreaterThanOrEqual, // Greater than or equal
		Like, // Like. The value can contain the SQL wildcard characters when like is specified.
		LessThan, // Less than
		LessThanOrEqual, // Less than or equal
		MoreOrEqual, // More or equal
		NotEqual, // Not equal
		NotWithinSet, // A value that is not within a set of values.
		NotIn, // Not in. The value can contain a comma-separated list of values.
		NotNull, // Not null
		IsNull, // Null
		To // The end of a range. Must be used with from.
	}

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