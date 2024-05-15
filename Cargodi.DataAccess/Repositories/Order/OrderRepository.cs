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
			.AsNoTracking()
			.AnyAsync(o => o.Id == id, cancellationToken);
	}

	public async Task<IEnumerable<Entities.Order.Order>> GetOrdersWithPayloadsAsync(CancellationToken cancellationToken)
	{
		return await _db.Orders
			.Include(o => o.LoadAddress)
			.Include(o => o.DeliverAddress)
			.Include(o => o.Payloads)
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
}