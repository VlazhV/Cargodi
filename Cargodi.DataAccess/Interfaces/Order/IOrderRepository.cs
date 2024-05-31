namespace Cargodi.DataAccess.Interfaces.Order;

public interface IOrderRepository: IRepository<Entities.Order.Order, long>
{
    Task<Entities.Order.Order?> SetStatusAsync(Entities.Order.Order order, string status, CancellationToken cancellationToken);
    void ClearPayloadList(Entities.Order.Order order);
    Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Entities.Order.Order>> GetAcceptedOrdersWithPayloadsAsync(CancellationToken cancellationToken);
    
    Task<IEnumerable<Entities.Order.Order>> GetAllOfClientAsync(long userId, CancellationToken cancellationToken);

    Task<List<Entities.Order.Order>> GetAllOfShipAsync(Entities.Ship.Ship ship, CancellationToken cancellationToken);
    void UpdateAcceptedOrdersToCompleting();
    void UpdateRange(IEnumerable<Entities.Order.Order> orders);
}