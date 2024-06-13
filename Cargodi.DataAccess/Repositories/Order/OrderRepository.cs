using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Order;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Order;

public class OrderRepository : RepositoryBase<Entities.Order.Order, long>, IOrderRepository
{
    public OrderRepository(DatabaseContext db) : base(db)
    {
    }

    public void ClearPayloadList(Entities.Order.Order order)
    {
        _db.Payloads.RemoveRange(order.Payloads);	
    }

    public async Task<bool> DoesItExistAsync(long id, CancellationToken cancellationToken)
    {
        return await _db.Orders
             
            .AnyAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Entities.Order.Order>> GetAllOfClientAsync(long userId, CancellationToken cancellationToken)
    {
        return await _db.Orders
             
            .Include(o => o.LoadAddress)
            .Include(o => o.DeliverAddress)
            .Include(o => o.Client)
                .ThenInclude(c => c.User)
            .Include(o => o.Payloads)
            .Include(o => o.OrderStatus)
            .Include(o => o.Operator)
            .Where(o => o.Client.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Entities.Order.Order>> GetAcceptedOrdersWithPayloadsAsync(CancellationToken cancellationToken)
    {
        return await _db.Orders
             
            .Include(o => o.LoadAddress)
            .Include(o => o.DeliverAddress)
            .Include(o => o.Payloads)
                .ThenInclude(p => p.PayloadType)
            .Include(o => o.Client)
            .Include(o => o.Operator)
            .Include(o => o.OrderStatus)
            .Where(o => o.OrderStatusId == OrderStatuses.Accepted.Id)
            .ToListAsync(cancellationToken);
    }


    public async Task<Entities.Order.Order?> SetStatusAsync(Entities.Order.Order order, string status, CancellationToken cancellationToken)
    {
        var statusEntity = await _db.OrderStatuses.FirstOrDefaultAsync(s => s.Name.Equals(status), cancellationToken);

        if (statusEntity is null)
            return null;

        order.OrderStatus = statusEntity;
        var entry = _db.Orders.Update(order);

        return entry.Entity;
    }

    public override async Task<IEnumerable<Entities.Order.Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Orders
             
            .Include(o => o.LoadAddress)
            .Include(o => o.DeliverAddress)
            .Include(o => o.Payloads)
                .ThenInclude(p => p.PayloadType)
            .Include(o => o.Client)
            .Include(o => o.Operator)
            .Include(o => o.OrderStatus)
            .ToListAsync(cancellationToken);
    }

    public override Task<Entities.Order.Order?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _db.Orders
             
            .Include(o => o.LoadAddress)
            .Include(o => o.DeliverAddress)
            .Include(o => o.Payloads)
                .ThenInclude(p => p.PayloadType)
            .Include(o => o.Client)
            .Include(o => o.Operator)
            .Include(o => o.OrderStatus)
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<Entities.Order.Order>> GetAllOfShipAsync(Entities.Ship.Ship ship, CancellationToken cancellationToken)
    {
        return _db.Orders
             
            .Include(o => o.Payloads)
                .ThenInclude(p => p.PayloadType)
            .Where(o => ship.Stops.Select(stop => stop.OrderId).Contains(o.Id))
            .ToListAsync(cancellationToken);
    }

    public void UpdateAcceptedOrdersToCompleting()
    {
        var orders = _db.Orders
            .Include(o => o.OrderStatus)
            .Where(o => o.OrderStatusId == OrderStatuses.Accepted.Id);

        var ordersToUpdate = new List<Entities.Order.Order>();
        
        foreach(var o in orders)
        {
            o.OrderStatusId = OrderStatuses.Completing.Id;
            ordersToUpdate.Add(o);
        }
        _db.Orders.UpdateRange(ordersToUpdate);
    }

    public void UpdateRange(IEnumerable<Entities.Order.Order> orders)
    {
        _db.Orders.UpdateRange(orders);
    }
}