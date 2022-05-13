#nullable enable
using System.Collections.Generic;
using System.Globalization;
using System.Net.Cache;
using JsonExts.JsonPath;
using Magento.RestClient.Authentication;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace Magento.RestClient
{
	public static class MagentoRestClientFactory
	{
		public static IRestClient CreateClient(string host, string? defaultScope)
		{
			RestSharp.RestClient client;

			if (host.EndsWith("/"))
			{
				host = host.TrimEnd('/');
			}

			if (string.IsNullOrWhiteSpace(defaultScope))
			{
				client = new RestSharp.RestClient($"{host}/rest/V1/")
				{
					CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate),
				};
			}
			else
			{
				client = new RestSharp.RestClient($"{host}/rest/{{scope}}/V1/")
				{
					CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate),
				};

				client.AddDefaultUrlSegment("scope", string.Empty);
			}


			var jsonSerializerSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.Indented,
				DateFormatString = "yyyy-MM-dd hh:mm:ss",
				DefaultValueHandling = DefaultValueHandling.Ignore,
				Converters = new List<JsonConverter> {
					//new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal},
					new JsonPathObjectConverter()
				},
			};

			client.UseNewtonsoftJson(jsonSerializerSettings);
			return client;
		}

		public static IRestClient CreateAdminClient(string host, string username, string password,
			string? defaultScope)
		{
			var client = CreateClient(host, defaultScope);
			var adminTokenUrl = $"{host}/rest/V1/integration/admin/token";

			client.Authenticator =
				new MagentoUserAuthenticator(adminTokenUrl, username, password, 4);
			return client;
		}

		public static IRestClient CreateIntegrationClient(string host,
			string consumerKey,
			string consumerSecret,
			string accessToken,
			string accessTokenSecret,
			string? defaultScope)
		{
			var client = CreateClient(host, defaultScope);

			client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey,
				consumerSecret, accessToken,
				accessTokenSecret);

			client.PreAuthenticate = true;
			return client;
		}

		public static IRestClient CreateCustomerClient(string host,
			string username,
			string password,
			string? defaultScope)
		{
			var customerTokenUrl = host + "/rest/V1/integration/customer/token";

			var client = CreateClient(host, defaultScope);
			client.Authenticator =
				new MagentoUserAuthenticator(customerTokenUrl, username, password, 1);


			client.PreAuthenticate = true;
			return client;
		}
	}
}