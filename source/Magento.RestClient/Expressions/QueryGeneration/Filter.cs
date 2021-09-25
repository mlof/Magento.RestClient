using System;
using Magento.RestClient.Search;

namespace Magento.RestClient.Expressions.QueryGeneration
{
	public class Filter
	{
		public Filter()
		{
		}

		public Filter(Filter currentFilter)
		{
			this.Value = currentFilter.Value;
			this.Condition = currentFilter.Condition;
			this.PropertyName = currentFilter.PropertyName;
		}

		public object Value { get; set; }

		public SearchCondition Condition { get; set; } = SearchCondition.Equals;

		public string PropertyName { get; set; }
		public Type PropertyType { get; set; }
	}
}