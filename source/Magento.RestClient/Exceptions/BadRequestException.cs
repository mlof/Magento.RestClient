using System;

namespace Magento.RestClient.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}