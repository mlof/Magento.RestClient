using System.Text;

namespace Magento.RestClient.Expressions
{
	public static class StringExtensions
	{
		public static string CreateMd5(this string input)
		{
			// Use input string to calculate MD5 hash
			using var md5 = System.Security.Cryptography.MD5.Create();
			var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			var hashBytes = md5.ComputeHash(inputBytes);

			// Convert the byte array to hexadecimal string
			var sb = new StringBuilder();
			// ReSharper disable once ForCanBeConvertedToForeach
			for (var i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("X2"));
			}

			return sb.ToString();
		}
	}
}