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

    public Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
    {
        return _db.Ships
			.AsNoTracking()
			.AnyAsync(ship => ship.Id == id, cancellationToken);
    }
}