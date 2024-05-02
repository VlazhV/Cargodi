namespace Cargodi.DataAccess.Interfaces.Order;

public interface IOrderRepository: IRepository<Entities.Order.Order, int>
{
    Task<Entities.Order.Order?> SetStatusAsync(Entities.Order.Order order, string status, CancellationToken cancellationToken);
	void ClearPayloadList(Entities.Order.Order order, CancellationToken cancellationToken);
	Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken);
}