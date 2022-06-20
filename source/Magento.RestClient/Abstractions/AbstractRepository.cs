using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Exceptions.Generic;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using Serilog.Events;

namespace Magento.RestClient.Abstractions
{
	public abstract class AbstractRepository
	{
		protected readonly IMagentoContext MagentoContext;

		protected AbstractRepository(IMagentoContext magentoContext)
		{
			MagentoContext = magentoContext;
		}

		protected RestSharp.RestClient Client => MagentoContext.RestClient;
		protected IMemoryCache Cache => MagentoContext.Cache;

		protected ILogger Logger => MagentoContext.Logger;


		/// <summary>
		///     Executes the request and parses the results.
		/// </summary>
		/// <param name="request"></param>
		/// <exception cref="MagentoException"></exception>
		async  protected Task<T> ExecuteAsync<T>(RestRequest request)
		{
			var sw = Stopwatch.StartNew();
			var response = await this.Client.ExecuteAsync<T>(request).ConfigureAwait(false);

			sw.Stop();


			if (!response.IsSuccessful)
			{
				if (response.StatusCode == HttpStatusCode.NotFound)
				{
					LogRequest(LogEventLevel.Verbose, response, sw);
					
				}
				else
				{
					LogRequest(LogEventLevel.Error, response, sw);
				}

				if (response.ErrorException is { } and not JsonSerializationException)
				{
					throw response.ErrorException;
				}

				if (response.StatusCode == HttpStatusCode.NotFound)
				{
					return default;
				}

				throw MagentoException.Parse(response.Content);
			}

			LogRequest(LogEventLevel.Verbose, response, sw);

			return response.Data;
		}


		/// <summary>
		///     ExecuteAsync
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <exception cref="MagentoException"></exception>
		async protected Task ExecuteAsync(RestRequest request)
		{
			var sw = Stopwatch.StartNew();
			var response = await this.Client.ExecuteAsync
				(request).ConfigureAwait(false);

			sw.Stop();

			if (response.IsSuccessful)
			{
				LogRequest(LogEventLevel.Verbose, response, sw);
			}

			else
			{
				LogRequest(LogEventLevel.Error, response, sw);

				if (response.ErrorException != null && response.ErrorException is not JsonSerializationException)
				{
					throw response.ErrorException;
				}

				var ex = MagentoException.Parse(response.Content);
				throw ex;
			}
		}

		private void LogRequest(LogEventLevel level, RestResponse response, Stopwatch sw)
		{
			Debug.Assert(response.Request != null, "response.Request != null");
			this.Logger.Write(level, "{StatusCode}\t{Method}\t{Scope}\t{Elapsed} ms\t{Uri}",
				response.StatusCode,
				response.Request.Method,
				response.Request.Parameters.SingleOrDefault(parameter => parameter.Name == "scope")?.Value,
				sw.Elapsed.Milliseconds,
				response.Request.Resource
			);
		}
	}
}