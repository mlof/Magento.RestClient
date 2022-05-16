namespace Magento.RestClient.Search
{
	public enum SearchCondition
	{
		/// <Summary>
		///     Equals
		/// </Summary>
		Equals,

		/// <Summary>
		///     A value within a set of values
		/// </Summary>
		InSet,

		/// <Summary>
		///     The beginning of a range. Must be used with to.
		/// </Summary>
		From,

		/// <Summary>
		///     Greater than
		/// </Summary>
		GreaterThan,

		/// <Summary>
		///     Greater than or equal
		/// </Summary>
		GreaterThanOrEqual,

		/// <Summary>
		///     Like. The value can contain the SQL wildcard characters when like is specified.
		/// </Summary>
		Like,

		/// <Summary>
		///     Less than
		/// </Summary>
		LessThan,

		/// <Summary>
		///     Less than or equal
		/// </Summary>
		LessThanOrEqual,

		/// <Summary>
		///     More or equal
		/// </Summary>
		MoreOrEqual,

		/// <Summary>
		///     Not equal
		/// </Summary>
		NotEqual,

		/// <Summary>
		///     A value that is not within a set of values.
		/// </Summary>
		NotWithinSet,

		/// <Summary>
		///     Not in. The value can contain a comma-separated list of values.
		/// </Summary>
		NotIn,

		/// <Summary>
		///     Not null
		/// </Summary>
		NotNull,

		/// <Summary>
		///     Null
		/// </Summary>
		IsNull,

		/// <Summary>
		///     The end of a range. Must be used with from.
		/// </Summary>
		To
	}
}