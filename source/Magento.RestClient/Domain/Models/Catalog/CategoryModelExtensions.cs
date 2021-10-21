namespace Magento.RestClient.Domain.Models.Catalog
{
	public static class CategoryModelExtensions
	{
		public static ICategoryModel GetOrCreateByPath(this ICategoryModel model, string path, char separator = '/')
		{
			var p = path.Trim(separator).Split(separator);
			var current = model;
			foreach (var s in p)
			{
				current = current.GetOrCreateChild(s);
			}

			return current;
		}
	}
}