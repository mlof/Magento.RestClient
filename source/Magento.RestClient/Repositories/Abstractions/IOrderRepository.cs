namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IOrderRepository : IReadOrderRepository, IWriteOrderRepository
    {
        void CreateInvoice(long orderId);
    }
}