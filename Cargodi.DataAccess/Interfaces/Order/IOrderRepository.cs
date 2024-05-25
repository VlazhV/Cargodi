namespace Cargodi.DataAccess.Interfaces.Order;

public interface IOrderRepository: IRepository<Entities.Order.Order, long>
{
    Task<Entities.Order.Order?> SetStatusAsync(Entities.Order.Order order, string status, CancellationToken cancellationToken);
    void ClearPayloadList(Entities.Order.Order order);
    Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Entities.Order.Order>> GetOrdersWithPayloadsAsync(CancellationToken cancellationToken);
    
    Task<IEnumerable<Entities.Order.Order>> GetAllOfClientAsync(long userId, CancellationToken cancellationToken);
}