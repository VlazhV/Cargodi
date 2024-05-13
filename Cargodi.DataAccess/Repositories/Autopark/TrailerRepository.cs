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
	
	public async Task<IEnumerable<Trailer>> GetFreeTrailersAsync(CancellationToken cancellationToken)
	{
		return await _db.Ships
			.Include(ship => ship.Trailer)
			.Where(ship => ship.Start != null && ship.Finish == null)
			.Select(ship => ship.Trailer)
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Trailer>> GetSuitableTrailersOrderedAsync(int weight, int volume, int biggestLinearSize, int autoparkStartId, CancellationToken cancellationToken)
	{
		return await _db.Ships
			.Include(ship => ship.Trailer)
			.Where(ship => ship.Start != null && ship.Finish == null)
			.Select(ship => ship.Trailer)
				.Where(car => car.Carrying > weight && car.Capacity() > volume && car.CanInclude(biggestLinearSize) && car.ActualAutoparkId == autoparkStartId)
				.OrderBy(trailer => trailer.Capacity())
			.ToListAsync(cancellationToken);
	}

}