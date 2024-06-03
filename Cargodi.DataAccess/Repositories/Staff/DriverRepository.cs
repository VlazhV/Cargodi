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

    public async Task<List<Driver>> GetSuitableDriversAsync(IEnumerable<Category> categories, int? shipId, CancellationToken cancellationToken)
    {
        var drivers = await _db.Drivers
            .Include(d => d.DriverCategories)
                .ThenInclude(dc => dc.Category)
            .Include(d => d.Ships)
            .ToListAsync(cancellationToken);

        return drivers.Where(d =>
                d.DriverCategories
                .Select(dc => dc.Category.Name)
                .Intersect(categories.Select(c => c.Name))
                .Any())
            // .Where(d => d.Ships == null 
            //     || d.Ships.Count == 0 
            //     || d.Ships.Last().Id == shipId 
            //     || d.Ships.Last().Finish != null)
            // .Where(d => d.DriverStatusId == DriverStatuses.Works.Id)
            .ToList();
    
    }
    
    public async Task<IEnumerable<Driver>> GetDriversByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        return await _db.Drivers
            .Where(d => ids.Contains(d.Id))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Driver>> GetAllAsync(bool? isFree, 
        bool? works, 
        int? actualAutoparkId, 
        CancellationToken cancellationToken)
    {
        var drivers = _db.Drivers
            .AsNoTracking()
            .Include(d => d.DriverStatus)
            .Where(d => !works.HasValue || d.DriverStatusId == DriverStatuses.Works.Id)
            .Where(d => !isFree.HasValue || d.ActualAutoparkId == actualAutoparkId!.Value);
            
        if (isFree.HasValue){
            drivers = drivers
                .Include(d => d.Ships)
                .Where(d => d.Ships!.Last().Finish != null);
        }

        return await drivers.ToListAsync(cancellationToken);
    }

}