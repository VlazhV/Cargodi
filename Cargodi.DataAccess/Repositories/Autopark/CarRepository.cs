using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Autopark;

public class CarRepository : RepositoryBase<Car, int>, ICarRepository
{
    public CarRepository(DatabaseContext db) : base(db)
    {
    }

    public async Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
    {
       return await _db.Cars
			.AsNoTracking()
			.AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken)
    {
        return await _db.Cars
			.AsNoTracking()
			.AnyAsync(c => c.LicenseNumber.Equals(licenseNumber), cancellationToken);
    }
}