using Magento.RestClient.Domain;
using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IWriteAttributeSetRepository
    {
        void Create(EntityType entityTypeCode, int skeletonId, AttributeSet attributeSet);

    }
}