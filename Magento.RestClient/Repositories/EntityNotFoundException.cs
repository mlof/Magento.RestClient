using System;

namespace Magento.RestClient.Repositories
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type type, string id) : base($"{type.Name} with Id {id} not found.")
        {
        }
    }
}