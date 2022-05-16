using JsonExts.JsonPath;
using Magento.RestClient.Authentication;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using RestSharp.Serializers.NewtonsoftJson;
using System.Collections.Generic;
using System.Globalization;

namespace Magento.RestClient
{
    public static class MagentoRestClientFactory
    {
        public static RestSharp.RestClient CreateClient(string host, string defaultScope = "default")
        {
            if (host.EndsWith("/"))
            {
                host = host.TrimEnd('/');
            }

            var baseUrl = $"{host}/rest/{{scope}}/V1/";
            var client = new RestSharp.RestClient(baseUrl)
            {
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Culture = CultureInfo.InvariantCulture,
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd hh:mm:ss",
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    //new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal},
                    new JsonPathObjectConverter()
                },
            };

            if (string.IsNullOrWhiteSpace(defaultScope))
            {
                client.AddDefaultUrlSegment("scope", "default");
            }
            else
            {
                client.AddDefaultUrlSegment("scope", defaultScope);
            }

            client.UseNewtonsoftJson(jsonSerializerSettings);
            return client;
        }

        public static RestSharp.RestClient CreateAdminClient(string host, string username, string password,
            string defaultScope = "default")
        {
            var client = CreateClient(host, defaultScope);
            var adminTokenUrl = $"{host}/rest/V1/integration/admin/token";

            client.Authenticator =
                new MagentoUserAuthenticator(adminTokenUrl, username, password, 4);
            return client;
        }

        public static RestSharp.RestClient CreateIntegrationClient(string host,
            string consumerKey,
            string consumerSecret,
            string accessToken,
            string accessTokenSecret,
            string defaultScope = "default")
        {
            var client = CreateClient(host, defaultScope);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey,
                consumerSecret, accessToken,
                accessTokenSecret, OAuthSignatureMethod.HmacSha256);

            return client;
        }

        public static RestSharp.RestClient CreateCustomerClient(string host,
            string username,
            string password,
            string defaultScope = "default")
        {
            var customerTokenUrl = host + "/rest/V1/integration/customer/token";

            var client = CreateClient(host, defaultScope);
            client.Authenticator =
                new MagentoUserAuthenticator(customerTokenUrl, username, password, 1);

            return client;
        }
    }
}