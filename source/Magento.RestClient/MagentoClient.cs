using System.Collections.Generic;
using System.Globalization;
using Magento.RestClient.Abstractions;
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
		private readonly string baseUrl;
		private readonly string adminTokenUrl;
		private readonly string customerTokenUrl;

		public MagentoClient(string host)
		{
			this.baseUrl = "";
			if (!host.EndsWith("/"))
			{
				host += "/";
			}

			this.baseUrl = host + "rest/{scope}/V1/";
			this.adminTokenUrl = host + "rest/V1/integration/admin/token";
			this.customerTokenUrl = host + "rest/V1/integration/customer/token";
			this._client = new RestSharp.RestClient(baseUrl);


			_client.AddDefaultUrlSegment("scope", "default");
			_client.UseNewtonsoftJson(new JsonSerializerSettings() {
				NullValueHandling = NullValueHandling.Ignore,
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.Indented,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				Converters = new List<JsonConverter>() {
					new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
				}
			});
		}


		public IAdminClient AuthenticateAsIntegration(string consumerKey, string consumerSecret,
			string accessToken,
			string accessTokenSecret)
		{
			_client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey,
				consumerSecret, accessToken,
				accessTokenSecret);

			return new AdminClient(_client);
		}

		public IAdminClient AuthenticateAsAdmin(string username, string password)
		{
			_client.Authenticator =
				new MagentoUserAuthenticator(adminTokenUrl, username, password, 4);

			return new AdminClient(_client);
		}

		public ICustomerClient AuthenticateAsCustomer(string username, string password)
		{
			_client.Authenticator =
				new MagentoUserAuthenticator(customerTokenUrl, username, password, 1);

			return new CustomerClient(_client);
		}

		public IGuestClient AuthenticateAsGuest()
		{
			return new GuestClient(_client);
		}
	}
}