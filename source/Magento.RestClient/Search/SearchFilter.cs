namespace Magento.RestClient.Search
{
	public class SearchFilter
	{
		public SearchFilter(string propertyName, SearchCondition condition, object value)
		{
			this.PropertyName = propertyName;
			this.Condition = condition;
			this.Value = value;
		}

		public object Value { get; set; }

		public SearchCondition Condition { get; set; }

		public string PropertyName { get; set; }
	}
}