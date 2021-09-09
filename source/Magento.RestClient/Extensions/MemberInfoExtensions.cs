using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Magento.RestClient.Extensions
{
	public static class MemberInfoExtensions
	{
		public static string GetPropertyName(this MemberInfo info)
		{
			var p = info.GetCustomAttributes(false).OfType<JsonPropertyAttribute>()
				.SingleOrDefault()?.PropertyName;
			return p ?? info.Name;
		}
	}
}