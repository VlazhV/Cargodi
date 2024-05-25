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

    public async Task<Entities.Ship.Ship?> GetShipFullInfoByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Ships
            .AsNoTracking()
            .Include(ship => ship.Stops)
                .ThenInclude(stop => stop.Order)
                    .ThenInclude(order => order.Payloads)
            .Include(ship => ship.Drivers)
            .Include(ship => ship.Car)
            .Include(ship => ship.Trailer)
            .Include(ship => ship.AutoparkStart)
            .Include(ship => ship.AutoparkFinish)
            
            .FirstOrDefaultAsync(ship => ship.Id == id, cancellationToken);
    }
    
    public async Task CreateManyAsync(IEnumerable<Entities.Ship.Ship> ships, 
        CancellationToken cancellationToken)
    {
        await _db.Ships.AddRangeAsync(ships, cancellationToken);
    }

    public async Task<IEnumerable<Entities.Ship.Ship>> GetAllShipsFullInfoAsync(CancellationToken cancellationToken)
    {
        return await _db.Ships
            .Include(ship => ship.Stops)                
                .ThenInclude(stop => stop.Order)
                    .ThenInclude(order => order.Payloads)
            .Include(ship => ship.Drivers)                
            .Include(ship => ship.Car)
            .Include(ship => ship.Trailer)
            .Include(ship => ship.AutoparkStart)
            .Include(ship => ship.AutoparkFinish)
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveAllStopsOfShipAsync(int shipId, CancellationToken cancellationToken)
    {
        var stops = await _db.Stops
            .Include(stop => stop.Ship)
            .Where(s => s.Ship.Id == shipId)
            .ToListAsync(cancellationToken);

        _db.Stops.RemoveRange(stops);
    }

    public async Task<IEnumerable<Entities.Ship.Ship>> GetShipsFullInfoOfDriverAsync(long userId, CancellationToken cancellationToken)
    {
        return await _db.Ships
            .Include(ship => ship.Stops)                
                .ThenInclude(stop => stop.Order)
                    .ThenInclude(order => order.Payloads)
            .Include(ship => ship.Drivers)
            .Include(ship => ship.Car)
            .Include(ship => ship.Trailer)
            .Include(ship => ship.AutoparkStart)
            .Include(ship => ship.AutoparkFinish)
            .Where(ship => ship.Drivers.Select(d => d.UserId).Contains(userId))
            .ToListAsync(cancellationToken);
    }
}