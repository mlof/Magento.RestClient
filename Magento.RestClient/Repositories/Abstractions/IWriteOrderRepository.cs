namespace MagentoApi.Repositories.Abstractions
{
    public interface IWriteOrderRepository
    {
        void Cancel(int orderId);
        void Hold(int orderId);
        void Unhold(int orderId);
        void Refund(int orderId);
        void Ship(int orderId);
    }
}