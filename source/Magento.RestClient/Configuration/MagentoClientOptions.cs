#nullable enable
namespace Magento.RestClient.Configuration
{
	public class MagentoClientOptions
	{
		public MagentoClientOptions()
		{
		}


		public MagentoClientOptions(string host, string username, string password,
			AuthenticationMethod authenticationMethod, string? defaultScope)
		{
			this.Host = host;
			this.Username = username;
			this.Password = password;
			this.DefaultScope = defaultScope;
			this.AuthenticationMethod = authenticationMethod;
		}

		public MagentoClientOptions(string host, string consumerKey, string accessTokenSecret, string accessToken,
			string consumerSecret, string? defaultScope)
		{
			this.Host = host;
			this.DefaultScope = defaultScope;
			this.ConsumerKey = consumerKey;
			this.AccessTokenSecret = accessTokenSecret;
			this.AccessToken = accessToken;
			this.ConsumerSecret = consumerSecret;
		}

		public string? Host { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? DefaultScope { get; set; }
		public AuthenticationMethod AuthenticationMethod { get; set; }
		public string? ConsumerKey { get; set; }
		public string? AccessTokenSecret { get; set; }
		public string? AccessToken { get; set; }
		public string? ConsumerSecret { get; set; }
	}
}