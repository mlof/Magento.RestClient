using System;
using RestSharp;

namespace Magento.RestClient.Search.Extensions
{
	public static class RestRequestExtensions
	{
		public static IRestRequest Search<T>(this IRestRequest request, Action<SearchBuilder<T>> configure)
		{
			var searchBuilder = new SearchBuilder<T>(request);
			if (configure != null)
			{
				configure.Invoke(searchBuilder);
			}

			return searchBuilder.Build();
		}

		public static IRestRequest SetScope(this IRestRequest request, string scope)
		{
			request.AddOrUpdateParameter("scope", scope, ParameterType.UrlSegment);
			return request;
		}
	}
}