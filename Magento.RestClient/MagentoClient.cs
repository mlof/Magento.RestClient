using System;
using System.Collections.Generic;
using System.Globalization;
using MagentoApi.Abstractions;
using MagentoApi.Exceptions;
using MagentoApi.Extensions;
using MagentoApi.Models;
using MagentoApi.Repositories.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace MagentoApi
{
    public class MagentoClient : IMagentoClient
    {
        private readonly IRestClient _client;

        public MagentoClient(string baseUrl)
        {
            this._client = new RestClient(baseUrl);
            

            _client.UseNewtonsoftJson(new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter>()
                {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }

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
            ;

            return new IntegrationClient(_client);
        }

        public void AuthenticateAsAdmin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void AuthenticateAsCustomer(string username, string password)
        {
            throw new NotImplementedException();
        }

    }
}