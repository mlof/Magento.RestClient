using System.Collections.Generic;
using System.Globalization;
using System.Net.Cache;
using JsonExts.JsonPath;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Authentication;
using Magento.RestClient.Data.Repositories.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace Magento.RestClient
{
	public class MagentoClient : IMagentoClient
	{
		public IRestClient Client { get; }
		private readonly string _adminTokenUrl;
		private readonly string _baseUrl;
		private readonly string _customerTokenUrl;
		public readonly MemoryCache Cache;

		public MagentoClient(string host, string defaultScope = "default")
		{
			Cache = new MemoryCache(new MemoryCacheOptions());

			_baseUrl = "";
			if (host.EndsWith("/"))
			{
				host = host.TrimEnd('/');
			}

			_baseUrl = $"{host}/rest/{{scope}}/V1/";
			_adminTokenUrl = $"{host}/rest/V1/integration/admin/token";
			_customerTokenUrl = host + "/rest/V1/integration/customer/token";
			this.Client = new RestSharp.RestClient(_baseUrl) {
				CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate)
			};

			this.Client.AddDefaultUrlSegment("scope", defaultScope);
			this.Client.UseNewtonsoftJson(new JsonSerializerSettings {
				NullValueHandling = NullValueHandling.Ignore,
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.Indented,
				DateFormatString = "yyyy-MM-dd hh:mm:ss",
				DefaultValueHandling = DefaultValueHandling.Ignore,
				Converters = new List<JsonConverter> {
					//new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal},
					new JsonPathObjectConverter()
				}
			});
		}

		public IAdminContext AuthenticateAsIntegration(string consumerKey, string consumerSecret,
			string accessToken,
			string accessTokenSecret)
		{
			this.Client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey,
				consumerSecret, accessToken,
				accessTokenSecret);

			this.Client.PreAuthenticate = true;
			return new AdminContext(this);
		}

		public IAdminContext AuthenticateAsAdmin(string username, string password)
		{
			this.Client.Authenticator =
				new MagentoUserAuthenticator(_adminTokenUrl, username, password, 4);

			return new AdminContext(this);
		}

		public ICustomerContext AuthenticateAsCustomer(string username, string password)
		{
			this.Client.Authenticator =
				new MagentoUserAuthenticator(_customerTokenUrl, username, password, 1);

			return new CustomerContext(this);
		}

		public IGuestContext AuthenticateAsGuest()
		{
			return new GuestContext(this);
		}
	}
}