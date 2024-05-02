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
	public async Task<IEnumerable<Driver>> GetAvailableAsync(CancellationToken cancellationToken)
	{
		return await _db.Drivers
			.Include(d => d.Ships)
			.Include(d => d.DriverStatus)
			.Where(d => d.Ships!.First().Finish != null && d.DriverStatus == DriverStatuses.Works)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

}