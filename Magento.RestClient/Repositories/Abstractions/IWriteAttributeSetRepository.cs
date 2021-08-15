namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IWriteAttributeSetRepository
    {
        void Create(EntityType entityTypeCode, int skeletonId, AttributeSet attributeSet);

    }
}