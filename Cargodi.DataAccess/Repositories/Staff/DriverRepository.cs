using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Staff;

public class DriverRepository : RepositoryBase<Driver, int>, IDriverRepository
{
    public DriverRepository(DatabaseContext databaseContext): base(databaseContext)
    {	}

    public async Task<Driver?> CreateDriverAsync(Driver driver, CancellationToken cancellationToken)
    {
        var entry = await _db.Drivers.AddAsync(driver, cancellationToken);

        return entry.Entity;
    }

    public Task<Driver?> GetDriverByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        return _db.Drivers
            .AsNoTracking()
            .Where(d => d.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Driver>> GetSuitableDriversAsync(IEnumerable<Category> categories, CancellationToken cancellationToken)
    {
        return await _db.Drivers
            .Include(d => d.Categories)
            .Where(d => d.Categories.Intersect(categories).Any())
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Driver>> GetDriversByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        return await _db.Drivers
            .Where(d => ids.Contains(d.Id))
            .ToListAsync(cancellationToken);
    }

}