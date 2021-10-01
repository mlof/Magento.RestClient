using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public abstract class AbstractRepository
	{
		private readonly IContext _context;
		protected IRestClient Client => _context.Client;
		protected IMemoryCache Cache => _context.Cache;

		protected AbstractRepository(IContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Executes the request and parses the results.
		/// </summary>
		/// <param name="request"></param>
		/// <exception cref="MagentoException"></exception>
		/// <exception cref="Magento.RestClient.Data.Repositories.Abstractions.NotFoundException"></exception>
		protected async Task<T> ExecuteAsync<T>(IRestRequest request) 
		{
			var response = await this.Client.ExecuteAsync<T>(request).ConfigureAwait(false);
			if (!response.IsSuccessful)
			{
				if (response.ErrorException != null && response.ErrorException is not JsonSerializationException)
				{
					throw response.ErrorException;
				}
				else if (response.StatusCode == HttpStatusCode.NotFound)
				{

					return default; 
				}

				else
				{
					
					throw MagentoException.Parse(response.Content);
				}
			}
			
			else
			{
				return response.Data;
			}
		}

		protected async Task ExecuteAsync(IRestRequest request)
		{
			var response = await this.Client.ExecuteAsync
				(request).ConfigureAwait(false);
			if (!response.IsSuccessful)
			{
				if (response.ErrorException != null && response.ErrorException is not JsonSerializationException)
				{
					throw response.ErrorException;
				}
				else
				{
					throw MagentoException.Parse(response.Content);
				}
			}
		}
	}
}