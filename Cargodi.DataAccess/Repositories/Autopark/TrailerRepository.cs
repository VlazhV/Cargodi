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
			.AsNoTracking()
			.AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken)
    {
        return await _db.Trailers
			.AsNoTracking()
			.AnyAsync(t => t.LicenseNumber.Equals(licenseNumber), cancellationToken);
    }
}