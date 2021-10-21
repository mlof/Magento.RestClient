using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Exceptions.Generic;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using Serilog.Events;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public abstract class AbstractRepository
	{
		private readonly IContext _context;
		protected IRestClient Client => _context.Client;
		protected IMemoryCache Cache => _context.Cache;

		protected ILogger Logger => _context.Logger;

		protected AbstractRepository(IContext context)
		{
			_context = context;
		}


		/// <summary>
		/// Executes the request and parses the results.
		/// </summary>
		/// <param name="request"></param>
		/// <exception cref="MagentoException"></exception>
		protected async Task<T> ExecuteAsync<T>(IRestRequest request)
		{
			var sw = Stopwatch.StartNew();
			var response = await this.Client.ExecuteAsync<T>(request).ConfigureAwait(false);

			sw.Stop();



			if (!response.IsSuccessful)
			{
				if (response.StatusCode == HttpStatusCode.NotFound)
				{
					this.LogRequest(LogEventLevel.Verbose, response, sw);

				}
				else
				{
					this.LogRequest(LogEventLevel.Error, response, sw);

				}

				if (response.ErrorException is { } and not JsonSerializationException)
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
				this.LogRequest(LogEventLevel.Verbose, response, sw);

				return response.Data;
			}
		}


		protected async Task ExecuteAsync(IRestRequest request)
		{
			var sw = Stopwatch.StartNew();
			var response = await this.Client.ExecuteAsync
				(request).ConfigureAwait(false);

			sw.Stop();

			if (response.IsSuccessful)
			{
				this.LogRequest(LogEventLevel.Verbose, response, sw);
			}

			else
			{
				this.LogRequest(LogEventLevel.Error, response, sw);

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

		private void LogRequest(LogEventLevel level, IRestResponse response, Stopwatch sw)
		{
			Logger.Write(level, "{StatusCode}\t{Method}\t{Scope}\t{Elapsed} ms\t{Uri}",
				response.StatusCode,
				response.Request.Method,
				response.Request.Parameters.SingleOrDefault(parameter => parameter.Name == "scope")?.Value,
				sw.Elapsed.Milliseconds,
				response.Request.Resource
			);
		}
	}
}