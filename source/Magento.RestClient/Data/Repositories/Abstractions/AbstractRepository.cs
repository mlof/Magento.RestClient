using System.Net;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public abstract class AbstractRepository
	{
		private readonly IContext context;
		protected IRestClient Client => context.Client;
		protected IMemoryCache Cache => context.Cache;
		protected AbstractRepository(IContext context)
		{
			this.context = context;
		}

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

			else if (response.ErrorException != null && response.ErrorException is not JsonSerializationException)
			{
				throw response.ErrorException;
			}
			else if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}
			else
			{
				throw MagentoException.Parse(response.Content);
			}
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