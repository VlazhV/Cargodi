using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Autopark;

public class TrailerRepository : RepositoryBase<Trailer, int>, ITrailerRepository
{
    public TrailerRepository(DatabaseContext db) : base(db)
    {
    }

    public async Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Trailers
             
            .AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken)
    {
        return await _db.Trailers
             
            .AnyAsync(t => t.LicenseNumber.Equals(licenseNumber), cancellationToken);
    }
    
    public async Task<IEnumerable<Trailer>> GetFreeTrailersAsync(CancellationToken cancellationToken)
    {
        return await _db.Ships  
             
            .Include(ship => ship.Trailer)
            .Where(ship => ship.Start != null && ship.Finish == null)
            .Select(ship => ship.Trailer)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trailer>> GetSuitableTrailersOrderedAsync(int weight,
        int volume,
        int biggestLinearSize,
        int autoparkStartId,
        int? shipId,
        CancellationToken cancellationToken)
    {
        var trailers = await _db.Trailers
             
            .Include(trailer => trailer.Ships)
            .Where(trailer =>
                trailer.Carrying > weight
                && trailer.CapacityHeight * trailer.CapacityLength * trailer.CapacityWidth > volume
                &&
                    trailer.CapacityHeight > biggestLinearSize
                    || trailer.CapacityLength > biggestLinearSize
                    || trailer.CapacityWidth > biggestLinearSize
                &&
                trailer.ActualAutoparkId == autoparkStartId)
            .OrderBy(trailer => trailer.CapacityHeight * trailer.CapacityLength * trailer.CapacityWidth).ThenBy(trailer => trailer.Carrying)
            .ToListAsync(cancellationToken);

        return trailers;

        // return trailers.Where(t => t.Ships == null 
        //     || t.Ships.Count == 0 
        //     || t.Ships!.Last().Id == shipId
        //     || t.Ships!.Last().Finish != null);
    }

    public Task<Trailer?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken cancellationToken)
    {
        return _db.Trailers
             
            .Where(c => c.LicenseNumber.Equals(licenseNumber))
            .FirstOrDefaultAsync();
    }

    public Task<Trailer?> GetTrailerFullInfoByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _db.Trailers
             
            .Include(t => t.TrailerType)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trailer>> GetAllTrailersFullInfoAsync(CancellationToken cancellationToken)
    {
        return await _db.Trailers
             
            .Include(t => t.TrailerType)
            .ToListAsync(cancellationToken);
            
    }
}