using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Magento.RestClient.Authentication
{
    public class MagentoUserAuthenticator : IAuthenticator
    {
        private readonly string _password;
        private readonly int _maximumTokenAgeInHours;
        private readonly string _path;
        private readonly string _username;

        public MagentoUserAuthenticator(string path, string username, string password, int maximumTokenAgeInHours)
        {
            _path = path;
            _username = username;
            _password = password;
            _maximumTokenAgeInHours = maximumTokenAgeInHours;
        }

        private DateTime? _bearerTokenExpiration;
        private string _bearerToken;

        public async ValueTask Authenticate(RestSharp.RestClient client, RestRequest request)
        {
            if (!_bearerTokenExpiration.HasValue || _bearerTokenExpiration.Value < DateTime.Now)
            {
                await RefreshBearerToken();
            }

            if (request.Parameters.Any(p => p.Name != null && p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            request.AddHeader("Authorization", $"Bearer {_bearerToken}");
        }

        private async Task RefreshBearerToken()
        {
            var c = new RestSharp.RestClient();
            c.UseNewtonsoftJson();
            var authenticationRequest = new RestRequest(_path, Method.Post);

            authenticationRequest.AddJsonBody(new { username = _username, password = _password });
            var response = await c.ExecuteAsync<string>(authenticationRequest);
            _bearerToken = response.Data;

            _bearerTokenExpiration = DateTime.Now.AddHours(_maximumTokenAgeInHours);

            Log.ForContext<MagentoUserAuthenticator>()
                .ForContext(nameof(_username), _username)
                .Information(
                    "Authenticated to {Uri}:\n" +
                    "Bearer {BearerToken}\n" +
                    "Expiration {Expiration}",
                    c.BuildUri(authenticationRequest), _bearerToken,
                    _bearerTokenExpiration);
        }
    }
}