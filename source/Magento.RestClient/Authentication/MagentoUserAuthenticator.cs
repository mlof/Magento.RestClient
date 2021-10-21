using System;
using System.Diagnostics;
using System.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;

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

		public void Authenticate(IRestClient client, IRestRequest request)
		{
			if (!_bearerTokenExpiration.HasValue || _bearerTokenExpiration.Value < DateTime.Now)
			{
				RefreshBearerToken(client.BaseUrl);
			}

			if (request.Parameters.Any(p => p.Name != null && p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
			{
				return;
			}

			request.AddHeader("Authorization", $"Bearer {_bearerToken}");
		}

		private void RefreshBearerToken(Uri clientBaseUrl)
		{
			Debug.Assert(clientBaseUrl != null, nameof(clientBaseUrl) + " != null");
			var c = new RestSharp.RestClient(clientBaseUrl);

			var authenticationRequest = new RestRequest(_path, Method.POST);

			authenticationRequest.AddJsonBody(new { username = _username, password = _password });
			var response = c.Execute<string>(authenticationRequest);
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