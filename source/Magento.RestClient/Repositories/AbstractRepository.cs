using System.Net;
using Magento.RestClient.Exceptions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
	public abstract class AbstractRepository
	{
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

		protected void HandleResponse(IRestResponse response)
		{
			if (!response.IsSuccessful)
			{
				throw MagentoException.Parse(response.Content);
			}
		}
	}
}