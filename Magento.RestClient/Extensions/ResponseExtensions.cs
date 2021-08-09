using System;
using System.Net;
using MagentoApi.Exceptions;
using RestSharp;

namespace MagentoApi.Extensions
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