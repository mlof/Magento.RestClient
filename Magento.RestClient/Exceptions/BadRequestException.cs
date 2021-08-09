using System;

namespace MagentoApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}