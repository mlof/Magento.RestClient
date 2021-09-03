using System.Collections.Generic;
using System.Globalization;
using JsonExts.JsonPath;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Authentication;
using Magento.RestClient.Repositories.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace Magento.RestClient
{
	public class MagentoClient : IMagentoClient
	{
		private readonly IRestClient _client;
		private readonly string adminTokenUrl;
		private readonly string baseUrl;
		private readonly string customerTokenUrl;

		public MagentoClient(string host)
		{
			baseUrl = "";
			if (host.EndsWith("/"))
			{
				host = host.TrimEnd('/');
			}

			baseUrl = $"{host}/rest/{{scope}}/V1/";
			adminTokenUrl = $"{host}/rest/V1/integration/admin/token";
			customerTokenUrl = host + "/rest/V1/integration/customer/token";
			_client = new RestSharp.RestClient(baseUrl);


			_client.AddDefaultUrlSegment("scope", "default");
			_client.UseNewtonsoftJson(new JsonSerializerSettings {
				NullValueHandling = NullValueHandling.Ignore,
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.Indented,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				Converters = new List<JsonConverter> {
					new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal},
					new JsonPathObjectConverter()
				}
			});
		}


		public IAdminContext AuthenticateAsIntegration(string consumerKey, string consumerSecret,
			string accessToken,
			string accessTokenSecret)
		{
			_client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey,
				consumerSecret, accessToken,
				accessTokenSecret);

			return new AdminContext(_client);
		}

		public IAdminContext AuthenticateAsAdmin(string username, string password)
		{
			_client.Authenticator =
				new MagentoUserAuthenticator(adminTokenUrl, username, password, 4);

			return new AdminContext(_client);
		}

		public ICustomerContext AuthenticateAsCustomer(string username, string password)
		{
			_client.Authenticator =
				new MagentoUserAuthenticator(customerTokenUrl, username, password, 1);

			return new CustomerContext(_client);
		}

		public IGuestContext AuthenticateAsGuest()
		{
			return new GuestContext(_client);
		}
	}
}