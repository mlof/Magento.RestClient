using Magento.RestClient.Domain;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IWriteAttributeSetRepository
    {
        void Create(EntityType entityTypeCode, int skeletonId, AttributeSet attributeSet);

    }
}