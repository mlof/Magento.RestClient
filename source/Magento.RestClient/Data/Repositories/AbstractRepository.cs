using System.Net;
using Magento.RestClient.Exceptions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public abstract class AbstractRepository
	{
		/// <summary>
		///     Handles a magento response and returns the value.
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		/// <exception cref="MagentoException"></exception>
		protected T HandleResponse<T>(IRestResponse<T> response) where T : class
		{
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}

			throw MagentoException.Parse(response.Content);
		}

		/// <summary>
		///     Handles a magento response.
		/// </summary>
		/// <param name="response"></param>
		/// <exception cref="MagentoException"></exception>
		protected void HandleResponse(IRestResponse response)
		{
			if (!response.IsSuccessful)
			{
				throw MagentoException.Parse(response.Content);
			}
		}
	}
}