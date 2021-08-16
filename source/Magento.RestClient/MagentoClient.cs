using System;
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

        public MagentoClient(string baseUrl)
        {
            this._client = new RestSharp.RestClient(baseUrl);


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


        public IIntegrationClient AuthenticateAsIntegration(string consumerKey, string consumerSecret,
            string accessToken,
            string accessTokenSecret)
        {
            _client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey,
                consumerSecret, accessToken,
                accessTokenSecret);

            return new IntegrationClient(_client);
        }

        public void AuthenticateAsAdmin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public ICustomerClient AuthenticateAsCustomer(string username, string password)
        {

            return new CustomerClient(_client);
        }

        public IGuestClient AuthenticateAsGuest()
        {
	        return new GuestClient(_client);
        }
    }

    public class GuestClient : IGuestClient
    {
	    public GuestClient(IRestClient client)
	    {
			
	    }
    }
}