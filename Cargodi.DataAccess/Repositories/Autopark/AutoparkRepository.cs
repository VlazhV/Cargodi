using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Autopark;

public class AutoparkRepository : RepositoryBase<Entities.Autopark.Autopark, int>, IAutoparkRepository
{
    public AutoparkRepository(DatabaseContext db) : base(db)
    {
    }

    public async Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Autoparks
             
            .AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Entities.Autopark.Autopark>> GetAutoparksWithAddressesVehicleAsync(CancellationToken cancellationToken)
    {
        return await _db.Autoparks
             
            .Include(a => a.Address)
            .Include(a => a.Cars)
                .ThenInclude(c => c.CarType)
            .Include(a => a.ActualCars)
                .ThenInclude(c => c.CarType)
            .Include(a => a.Trailers)
                .ThenInclude(t => t.TrailerType)
            .Include(a => a.ActualTrailers)
                .ThenInclude(t => t.TrailerType)
            .Include(a => a.Drivers)
            .Include(a => a.ActualDrivers)
             
            .ToListAsync(cancellationToken);
    }

    public async Task<Entities.Autopark.Autopark?> GetAutoparkWithAddressesVehicleByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Autoparks
             
            .Include(a => a.Address)
            .Include(a => a.Cars)
                .ThenInclude(c => c.CarType)
            .Include(a => a.ActualCars)
                .ThenInclude(c => c.CarType)
            .Include(a => a.Trailers)
                .ThenInclude(t => t.TrailerType)
            .Include(a => a.ActualTrailers)
                .ThenInclude(t => t.TrailerType)
            .Include(a => a.Drivers)
            .Include(a => a.ActualDrivers)
             
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

}
