using System;

namespace MagentoApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string errorMessage, Exception innerException): base(errorMessage, innerException)
        {
        }
    }
}