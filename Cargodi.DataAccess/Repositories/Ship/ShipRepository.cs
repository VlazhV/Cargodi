using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Ship;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Ship;

public class ShipRepository : RepositoryBase<Entities.Ship.Ship, int>, IShipRepository
{
	public ShipRepository(DatabaseContext db) : base(db)
	{
	}

	public async Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
	{
		return await _db.Ships
			.AsNoTracking()
			.AnyAsync(ship => ship.Id == id, cancellationToken);
	}

	public async Task<Entities.Ship.Ship?> GetShipWithStopsWithOrdersAsync(int id, CancellationToken cancellationToken)
	{
		return await _db.Ships
			.Include(ship => ship.Stops)
				.ThenInclude(stop => stop.Order)
					.ThenInclude(order => order.Payloads)
			.FirstOrDefaultAsync(ship => ship.Id == id, cancellationToken);
	}

}