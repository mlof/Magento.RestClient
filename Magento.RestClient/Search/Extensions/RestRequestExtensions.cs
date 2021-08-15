using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magento.RestClient.Models;
using RestSharp;

namespace Magento.RestClient.Search.Extensions
{
    public static class RestRequestExtensions
    {
        public static IRestRequest Search<T>(this IRestRequest request, Action<SearchBuilder<T>> configure)
        {
            var searchBuilder = new SearchBuilder<T>(request);
            if (configure != null)
            {
                configure.Invoke(searchBuilder);
            }

            return searchBuilder.Build();
        }
    }
}