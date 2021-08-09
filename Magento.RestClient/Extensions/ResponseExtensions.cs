using System;
using System.Net;
using Magento.RestClient.Exceptions;
using RestSharp;

namespace Magento.RestClient.Extensions
{
    public static class ResponseExtensions
    {
        public static Exception GetException(this IRestResponse response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestException(response.ErrorMessage, response.ErrorException),
                HttpStatusCode.NotFound => new NotFoundException(response.ErrorMessage, response.ErrorException),
                _ => response.ErrorException
            };
        }
    }
}