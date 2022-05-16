using System;

namespace Magento.RestClient.Exceptions.Generic
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(Type type, string id) : base($"{type.Name} with Id {id} not found.")
		{
		}

		public EntityNotFoundException()
		{
		}

		public EntityNotFoundException(string message) : base(message)
		{
		}

		public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}